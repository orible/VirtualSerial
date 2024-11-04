using ICSharpCode.AvalonEdit.Utils;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static VirtualSerial.MainForm;

namespace VirtualSerial
{
    public class VM
    {
        // data
        private string bufferedScript;
        private Script lScript;

        // main function address
        private DynValue fnMain;

        // callback timers
        Mutex mutTimers = new Mutex();
        Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

        Mutex mutInvokeCallbacks = new Mutex();
        Dictionary<string, VMFuncInvoke> invokeCallbacks = new Dictionary<string, VMFuncInvoke>();

        Mutex mutHookListeners = new Mutex();
        Dictionary<string, VMHooks> hookListeners = new Dictionary<string, VMHooks>();

        // in and out messages
        BlockingCollection<VMMessage> _in = new BlockingCollection<VMMessage>();
        BlockingCollection<VMMessage> _out = new BlockingCollection<VMMessage>();

        // tokens
        CancellationTokenSource cancelToken = new();

        // status
        volatile bool ThreadIsRunning = false, IsDisposed = false;
        void Log(string s)
        {
            CallListeners("_log", s);
        }

        void _DoMessage(VMMessage msg)
        {
            switch (msg.Code)
            {
                case VMMESSAGE.CALLFUNC:
                    {
                        VMMessageFuncCall call = ((VMMessageFuncCall)msg.Data);
                        // get global function and execute it
                        _call(call.Call, call.args);
                    }
                    break;
                case VMMESSAGE.CALLHOOK:
                    {
                        // get registered hook and execute functions matching filter
                        object[] call = ((object[])msg.Data);
                        _callHooks((string)call[0], call[1]);
                    }
                    break;
                case VMMESSAGE.DELEGATE:
                    {
                        VMDelegate call = ((VMDelegate)msg.Data);
                        object ret;
                        call.Func.Invoke(this, call.Args);
                    }
                    break;
            }
        }
        private void SendMessage(VMMessage data) => this._in.Add(data);      

        // tick VM thread until signalled to stop
        void _Thread(object ctx)
        {
            try
            {
                CallListeners("_start", null);
                ThreadCtx settings = (ThreadCtx)ctx;

                _Tick();

                while (!cancelToken.IsCancellationRequested)
                {
                    VMMessage item;
                    var msg = _in.TryTake(out item);
                    if (msg)
                    {
                        Log($"got message: {item.Code}");
                        CallListeners("_domessage");
                        _DoMessage(item);
                    }
                    // do tick
                    _Tick();
                    CallListeners("_tick", null);
                    Thread.Sleep(1000/5);
                }
                CallListeners("_stop", null);
            }
            catch (InterpreterException e)
            {
                Log(e.Message.ToString());
                CallListeners("_exception", e);
                CallListeners("_stop", null);
                return;
            } catch (Exception e)
            {
                Log(e.Message.ToString());
                CallListeners("_exception", e);
                CallListeners("_stop", null);
                return;
            }
        }
        void _Tick()
        {
            // tick all timers
            foreach (var e in timers)
            {
                e.Value.LastCall = DateTime.Now;
                e.Value.Func.Function.Call();
            }
        }
        Thread thread;

        public enum VMMESSAGE {
            STOP = 0,
            START = 1,
            CALLFUNC = 2,
            DELEGATE = 6,
            RECOMPILE_SCRIPT = 3,
            RUN_SCRIPT = 5,
            CALLHOOK = 4,
        }
        class Settings
        {
            public int TickRate = 15;
        }

        Settings settings = new();

        class ThreadCtx
        {
            public Settings Ops;
            public ThreadCtx(Settings settings)
            {
                Ops = settings;
            }
        }
        public class VMMessageFuncCall
        {
            public string Call;
            public object[] args;
            public VMMessageFuncCall(string call, object[] args)
            {
                Call = call;
                this.args = args;
            }
        }
        public enum VMEventCodeEnum
        {
            PRINT = 1,
            LOG = 2,
            ERROR = 3,
            FAULT = 4,
            EXCEPTION = 5,
            TICK = 10,
            FUNCCALL = 11,
            START = 13,
            STOP = 14,
            COMPILE = 15,
        }
        public class VMMessage {
            public VMMESSAGE Code;
            public object Data;
            public VMMessage(VMMESSAGE Code, object Data)
            {
                this.Code = Code;
                this.Data = Data;
            }
        }
        public class VMArgs
        {
            public object[] Args;
            VMArgs(object[] args)
            {
                this.Args = args;
            }
        }

        public delegate object? FuncInvoke(VM vm, object[] args);
        public class VMFuncInvoke : Callback
        {
            public string Filter;
            public FuncInvoke Func;
            public VMFuncInvoke(FuncInvoke Func, string Filter)
            {
                this.Filter = Filter;
                this.Func = Func;
            }
        }

        public class VMDelegate
        {
            public string UID = Guid.NewGuid().ToString();
            public FuncInvoke Func;
            public object[] Args;
            public VMDelegate(FuncInvoke Func, object[] Args)
            {
                this.Func = Func;
                this.Args = Args;
            }
        }

        private class VMHooks : Callback
        {
            public DynValue Func;
            public object[] Args;
            public VMHooks(DynValue Func, object[] Args)
            {
                this.Func = Func;
                this.Args = Args;
            }
        }
   
        public class Callback
        {
            public string UID;
            public string Name;
            public Callback()
            {
                UID = Guid.NewGuid().ToString();
            }
        }

        public class Timer: Callback
        {
            public DynValue Func;
            public int Delay;
            public int Repeat;
            public DateTime LastCall;
            public Timer(DynValue Func, int delay, int repeat) : base()
            {
                this.Delay = delay;
                this.Repeat = repeat;
                this.Func = Func;
            }
        }

        public class VMEvent
        {
            public VMEventCodeEnum Code;
            public object Data;
            public VMEvent(VMEventCodeEnum code, object Data) { 
                this.Code = code;
                this.Data = Data;
            }
        };

        public delegate void VMEventHandler(object sender, VMEvent e);
        public event VMEventHandler EventHandler;
        readonly string[] ReceiveEvents =
        {
            "REGISTER_RECEIVE_LINE",
        };

        private void _attachGlobals(ref Script vmScript)
        {
            // assign global functions - this will overwrite them if they're defined in the script
            vmScript.Globals["SEND"] = (Func<string, int>)lfuncSend;
            vmScript.Globals["TIMER"] = (Func<string, int, int, DynValue, int>)lfuncAddTimer;
            vmScript.Globals["REMOVETIMER"] = (Func<string, int>)lfuncRemoveTimer;
            vmScript.Globals["SETBUFFER"] = (Func<string, string, int>)lfuncSetBuffer;
            vmScript.Globals["REGISTER"] = (Func<string, DynValue, int>)lfuncRegisterHook;
            vmScript.Globals["REGISTER_RECEIVE_LINE"] = (Func<string, DynValue, int>)lfuncReceiveHook;
            vmScript.Globals["CONNECT"] = (Func<string, string, int, string, float, int, int, int, int>)lfuncConnect;
            vmScript.Globals["DISCONNECT"] = (Func<int>)lfuncDisconnect;
            vmScript.Globals["PRINT"] = (Func<string, int>)lfuncPrint;
            vmScript.Globals["DEBUG"] = (Func<string, int>)lfuncPrint;
            vmScript.Globals["WITEFILE"] = (Func<string, MoonSharp.Interpreter.Table, int>)lfuncWriteFile;
            vmScript.Globals["SETTICKRATE"] = (Func<int, int>)lfuncTickRate;
        }
        public void Shutdown()
        {
            IsDisposed = true;
            cancelToken.Cancel();
        }
        public bool UnsafeIsCompiled()
        {
            return this.fnMain != null;
        }
        public bool IsRunningThreaded()
        {
            return (thread != null && thread.IsAlive);
        }

        public void Stop()
        {
            if (cancelToken != null)
                cancelToken.Cancel();
        }
        public bool RunAsThreaded()
        {
            if (IsRunningThreaded()) return false;
            cancelToken = new();
            lfuncTickRate(15);
            thread = new Thread(new ParameterizedThreadStart(_Thread));
            thread.Start(new ThreadCtx(settings));
            return true;
        }
        public void UnsafeCompile()
        {
            if (bufferedScript == null || bufferedScript == "") return;
            Script vmScript = new Script();
            DynValue _fnMain = vmScript.LoadString(bufferedScript);
            _attachGlobals(ref vmScript);
            fnMain = _fnMain;
            lScript = vmScript;
            //UnsafeCall("main", null);
        }
        public void UnsafeSetScript(string buffer)
        {
            bufferedScript = buffer;
        }
        private int lfuncReceiveHook(string funcname, DynValue func)
        {
            //this.callbacks["REGISTER_RECEIVE_LINE_" + funcname] = new Callback(func);
            return -1;
        }
        private int lfuncSetBuffer(string a, string b)
        {
            CallListeners("setbuffer", a, b);
            // set buffer mode
            return -1;
        }
        private int lfuncRegisterHook(string func, DynValue val)
        {
            VMHooks hook = new VMHooks(val, null);
            this.hookListeners.Add(hook.UID, hook);
            return -1;
        }
        private int lfuncTickRate(int tickrate)
        {
            settings.TickRate = tickrate;
            return 1;
        }
        private int lfuncConnect(
            string port,
            string buffermode,
            int baud,
            string parity,
            float stop,
            int data,
            int readtimeout,
            int writetimeout)
        {
            CallListeners("connect", port, buffermode, baud, parity, stop, data, readtimeout, writetimeout);
            return 1;
        }
        private int lfuncDisconnect()
        {
            CallListeners("disconnect");
            return 1;
        }
        private int lfuncRemoveTimer(string name)
        {
            this.timers.Remove(name);
            return 1;
        }
        private int lfuncAddTimer(string name, int delay, int repeat, DynValue value)
        {
            var timer = new Timer(value, delay, repeat);
            this.timers.Add(name, timer);
            return -1;
        }
        private int lfuncWriteFile(string dir, MoonSharp.Interpreter.Table list)
        {
            string path = $"./output/{dir}";
            string dirpath = Path.GetFullPath(Path.GetDirectoryName(path));
            System.IO.Directory.CreateDirectory(dirpath);
            File.AppendAllLines(path, new string[] { list.TableToJson() });
            return 1;
        }
        private int lfuncPrint(string str)
        {
            CallListeners("PRINT", str);
            return 1;
        }
        private int lfuncSend(string str)
        {
            //byte[] buf = Encoding.ASCII.GetBytes(str);
            CallListeners("SEND", str);
            return 0;
        }

        private void UnsafeCallListeners(string filter, params object[] args)
        {
            filter = filter.ToUpper();
            foreach (var entry in this.invokeCallbacks)
            {
                if (entry.Value == null)
                    continue;

                VMFuncInvoke invoke = entry.Value;
                if (invoke.Filter.StartsWith(filter))
                {
                    invoke.Func.Invoke(this, args);
                }
            }
        }

        // invoke function callbacks registered to listen to when a function name is invoked in the lua program
        private void CallListeners(string filter, params object[] args)
        {
            filter = filter.ToUpper();
            mutInvokeCallbacks.WaitOne();
            foreach (var entry in this.invokeCallbacks)
            {
                if (entry.Value == null)
                    continue;

                VMFuncInvoke invoke = entry.Value;
                if (invoke.Filter.StartsWith(filter))
                {
                    invoke.Func.Invoke(this, args);
                }
            }
            mutInvokeCallbacks.ReleaseMutex();
        }

        // attaches a invoke callback and returns the callback ID
        // all function names are unique and upper case
        public string RegisterFunctionInvokeListener(string FunctionFilter, FuncInvoke action, params object[] ctx)
        {
            var filter = FunctionFilter.ToUpper();
            var invoke = new VMFuncInvoke(action, filter);
            mutInvokeCallbacks.WaitOne();
            invokeCallbacks.Add(invoke.UID, invoke);
            mutInvokeCallbacks.ReleaseMutex();
            return invoke.UID;
        }

        public void Invoke(FuncInvoke action, params object[] args)
        {
            //if (!IsRunningThreaded())
            //{
            //    action.Invoke(this, args);
            //    return;
            //}
            SendMessage(new VMMessage(VMMESSAGE.DELEGATE, new VMDelegate(action, args)));
        }

        private bool _call(string function, params object[] args)
        {
            if (this.fnMain == null)
                return false;

            if (function.ToLower() == "main")
            {
                CallListeners("_func", function, args);
                this.fnMain.Function.Call();
                CallListeners("_func_post", function, args);
            }

            var fn = this.lScript.Globals.Get(function);
            //var fn = this.lScript.Globals[function];
            if (fn == null || fn == DynValue.Nil)
                return false;
            CallListeners("_func", function, args);
            lScript.Call(fn, args);
            CallListeners("_func_post", function, args);
            return true;
        }

        private void _callHooks(string filter, params object[] args)
        {
            mutHookListeners.WaitOne();
            foreach (var e in hookListeners)
            {
                e.Value.Func.Function.Call(args);
            }
            mutHookListeners.ReleaseMutex();
        }

        // call listeners
        public void UnsafeCallHook(string hookfilter, params object[] args)
        {
            hookfilter = hookfilter.ToUpper();
            _callHooks(hookfilter, args);
        }

        // call will execute the specified script
        public void UnsafeCall(string function, params object[] args)
        {
            _call(function, args);
        }
    }
}
