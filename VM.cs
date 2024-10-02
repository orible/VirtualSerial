﻿using ICSharpCode.AvalonEdit.Utils;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static VirtualSerial.Form1;

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
        Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        Dictionary<string, VMFuncInvoke> invokeCallbacks = new Dictionary<string, VMFuncInvoke>();
        Dictionary<string, VMHooks> hookListeners = new Dictionary<string, VMHooks>();

        // in and out messages
        BlockingCollection<VMMessage> _in = new BlockingCollection<VMMessage>();
        BlockingCollection<VMMessage> _out = new BlockingCollection<VMMessage>();

        // tokens
        CancellationTokenSource cancelToken = new();
        
        // status
        volatile bool ThreadIsRunning = false, IsDisposed = false;

        void _DoMessage(VMMessage msg)
        {
            switch (msg.Code) 
            {
                case VMMESSAGE.CALLFUNC:
                    {
                        VMMessageFuncCall call = ((VMMessageFuncCall)msg.Data);
                        // get global function and execute it
                        string fnc = call.Call;
                        var fn = lScript.Globals[fnc];
                        lScript.Call(fn, call.args);
                        //CallRegistrations(fnc);
                    }
                    break;
                case VMMESSAGE.CALLHOOK:
                    {
                        // get registered hook and execute functions matching filter
                        object [] call = ((object[])msg.Data);
                        _callHooks((string)call[0], call[1]);
                    }
                    break;
                case VMMESSAGE.DELEGATE:
                    {
                        VMDelegate call = ((VMDelegate)msg.Data);
                        call.Func.Invoke(this, call.Args);
                    }
                    break;
            }
        }
        private void SendMessage(VMMessage data)
        {
            this._in.Add(data);
        }
        // loop registrations
        void _CallRegistrations(string func)
        {

        }
        // tick VM thread until signalled to stop
        void _Thread(object ctx)
        {
            Settings settings = (Settings)ctx;
            while (!cancelToken.IsCancellationRequested)
            {
                VMMessage item;
                var msg = _in.TryTake(out item);
                if (msg)
                {
                    _DoMessage(item);
                }
                // do tick
                _Tick();
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
            DELEGATE = 5,
            RECOMPILE_SCRIPT = 3,
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
            public VMMessage(VMMESSAGE Code, params object [] Data)
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
        public class VMFuncInvoke: Callback
        {
            public string Filter;
            public Action<VM, object[]> Func;
            public VMFuncInvoke(Action<VM, object[]> Func, string Filter)
            {
                this.Filter = Filter;
            }
        }
        public class VMDelegate
        {
            public string UID = Guid.NewGuid().ToString();
            public Action<VM, object[]> Func;
            public object[] Args;
            public VMDelegate(Action<VM, object[]> Func, object[] Args)
            {
                this.Func = Func;
                this.Args = Args;
            }
        }
        private class VMHooks
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
            public Timer(DynValue Func) : base()
            {
                this.Func = Func;
            }
        }
        public class VMEvent
        {
            public VMEventCodeEnum Code;
            public object Data;
            public VMEvent(VMEventCodeEnum code, object Data) { this.Code = code; this.Data = Data; }
        };
        public delegate void VMEventHandler(object sender, VMEvent e);
        public event VMEventHandler EventHandler;
        readonly string[] ReceiveEvents =
        {
            "REGISTER_RECEIVE_LINE",
        };
        private void _attachGlobals(ref Script vmScript)
        {
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
        public bool IsCompiled()
        {
            return this.fnMain != null;
        }
        public bool IsRunningThreaded()
        {
            return (thread != null && thread.IsAlive);
        }
        public void RunAsThreaded()
        {
            if (IsRunningThreaded()) return;
            cancelToken = new();
            lfuncTickRate(15);
            thread = new Thread(new ParameterizedThreadStart(_Thread));
            thread.Start(new ThreadCtx(settings));
        }
        public void Compile()
        {
            Script vmScript = new Script();
            DynValue _fnMain = vmScript.LoadString(bufferedScript);
            _attachGlobals(ref vmScript);
            fnMain = _fnMain;
            lScript = vmScript;
        }
        public void SetScript(string buffer)
        {
            bufferedScript = buffer;
        }
        public void RunAndTick()
        {
            fnMain.Function.Call();
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
            return -1;
        }
        private int lfuncDisconnect()
        {
            CallListeners("disconnect");
            return -1;
        }
        private int lfuncRemoveTimer(string name)
        {
            this.timers.Remove(name);
            return -1;
        }
        private int lfuncAddTimer(string name, int delay, int repeat, DynValue value)
        {
            var timer = new Timer(value);
            timer.Delay = delay;
            timer.Repeat = repeat;
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
            CallListeners("PRINT", new VMEvent(VMEventCodeEnum.PRINT, str));
            return 1;
        }
        private int lfuncSend(string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            CallListeners("SEND", buf);
            return 0;
        }
        private void CallListeners(string filter, params object[] args)
        {
            foreach(var cb in this.invokeCallbacks)
            {
                if (cb.Value.Filter.StartsWith(filter))
                {
                    cb.Value.Func.Invoke(this, args);
                }
            }
        }
        // attaches a invoke callback and returns the callback ID
        // all function names are unique and upper case
        public string RegisterFunctionInvokeListener(string FunctionFilter, Action<VM, object[]> action, params object[] ctx)
        {
            var filter = FunctionFilter.ToUpper();
            var invoke = new VMFuncInvoke(action, filter);
            invokeCallbacks.Add(invoke.UID, invoke);
            return invoke.UID;
        }
        public void Invoke(Action<VM, object[]> action, params object[] args)
        {
            if (!IsRunningThreaded())
            {
                action.Invoke(this, args);
                return;
            }
            SendMessage(new VMMessage(VMMESSAGE.DELEGATE, new VMDelegate(action, args)));
        }
        public  void CallList(object sender, string[] functions, VMArgs args)
        {

        }
        public void CallRegisterCallback(string function, params object[] args)
        {

        }
        private void _call(string function, params object[] args)
        {
            var fn = this.lScript.Globals[function];
            if (fn == null) return;
            lScript.Call(fn, args);
        }
        private void _callHooks(string filter, params object[] args)
        {
            foreach (var e in hookListeners)
            {
                e.Value.Func.Function.Call(args);
            }
        }
        public void CallHook(string hookfilter, params object[] args)
        {
            hookfilter = hookfilter.ToUpper();
            if (!IsCompiled()) return;
            if (IsRunningThreaded())
            {
                SendMessage(new VMMessage(VMMESSAGE.CALLHOOK, hookfilter, args));
                return;
            } 
            _callHooks(hookfilter, args);
        }
        // call will execute the specified script
        public void Call(string function, params object[] args)
        {
            function = function.ToUpper();
            if (!IsCompiled()) return;
            if (IsRunningThreaded())
            {
                SendMessage(new VMMessage(VMMESSAGE.CALLFUNC, function, args));
                return;
            }
            _call(function, args);
        }
    }
}
