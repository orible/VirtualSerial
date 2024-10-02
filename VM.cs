using ICSharpCode.AvalonEdit.Utils;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Dictionary<string, VMFuncInvoke> eventCallbacks = new Dictionary<string, VMFuncInvoke>();
        //List<VMInvokes> invokes = new VMInvokes();

        // in and out messages
        BlockingCollection<VMMessage> _in = new BlockingCollection<VMMessage>();
        BlockingCollection<VMMessage> _out = new BlockingCollection<VMMessage>();

        CancellationTokenSource cancelToken = new();
        volatile bool ThreadIsRunning = false, IsDisposed = false;

        void _DoMessage(VMMessage msg)
        {
            switch (msg.Code) 
            {
                case VMMESSAGE.CALLFUNC:
                    {
                        VMMessageFuncCall call = ((VMMessageFuncCall)msg.Data);
                        string fnc = call.Call;
                        var fn = lScript.Globals[fnc];
                        lScript.Call(fn, call.args);
                    }
                    break;
                case VMMESSAGE.CALLBACK:
                    {
                        VMDelegate call = ((VMDelegate)msg.Data);
                        call.Func.Invoke(this, call.Args);
                    }
                    break;
            }
        }

        // tick VM thread until signalled to stop
        void _Thread(object ctx)
        {
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
            foreach (var e in timers)
            {
                // tick all timers
                e.Value.Func.Function.Call();
            }
        }
        Thread thread;

        public enum VMMESSAGE {
            STOP = 0,
            START = 1,
            CALLFUNC = 2,
            RECOMPILE_SCRIPT = 3,
            CALLBACK = 4,
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
        //public class VMCallback {
        //    public Action<VM> Callback;
        //    public object [] Args;
        //}
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
            public int LastCall;
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
            vmScript.Globals["REGISTER_REEIVE"] = (Func<string, DynValue, int>)lfuncReceiveHook;
            vmScript.Globals["REGISTER_RECEIVE_LINE"] = (Func<string, DynValue, int>)lfuncReceiveHook;
            vmScript.Globals["CONNECT"] = (Func<string, string, int, string, float, int, int, int, int>)lfuncConnect;
            vmScript.Globals["DISCONNECT"] = (Func<int>)lfuncDisconnect;
            vmScript.Globals["print"] = (Func<string, int>)lfuncPrint;
            vmScript.Globals["debug"] = (Func<string, int>)lfuncPrint;
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
            EventHandler.Invoke(this, new VMEvent(VMEventCodeEnum.PRINT, str));
            return 1;
        }
        private int lfuncSend(string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            //this._out.Add(new Message(Message.MessageCode.NULL, buf, Message.SendAsEncoding.ASCII));
            CallListeners("send", str);
            return 0;
        }
        private void CallListeners(string filter, params object[] args)
        {
            foreach(var cb in this.eventCallbacks)
            {
                if (cb.Value.Filter.StartsWith(filter))
                {
                    cb.Value.Func.Invoke(this, args);
                }
            }
        }

        // attaches a invoke callback and returns the callback ID
        public string ListenFunctionInvoke(string FunctionFilter, Action<VM, object[]> action, params object[] ctx)
        {
            var filter = FunctionFilter.ToLower();
            var invoke = new VMFuncInvoke(action, filter);
            eventCallbacks.Add(invoke.UID, invoke);
            return invoke.UID;
        }
        public void Invoke(Action<VM, object[]> action, params object[] args)
        {
            if (!IsRunningThreaded())
            {
                action.Invoke(this, args);
                return;
            }

            var ev = new VMMessage();
            ev.Code = VMMESSAGE.CALLBACK;
            var vm = new VMDelegate(action, args);
            ev.Data = vm;
            this._in.Add(ev);
        }
        private void Call(object sender, VMMessage msg)
        {
            var m = new VMMessage();
            m.Code = VMMESSAGE.CALLFUNC;

            if (IsRunningThreaded())
            {
                // defer to caller
                this._in.Prepend(msg);
                return;
            }
            // immediate exectution
            VMMessageFuncCall call = ((VMMessageFuncCall)msg.Data);
            lScript.Call(call.Call, call.args);
        }
        public  void CallList(object sender, string[] functions, VMArgs args)
        {

        }
        // call will execute the specified script
        public void Call(string function, params object[] args)
        {
            if (!IsCompiled()) return;// throw Exception("Not Compiled");
            var fn = this.lScript.Globals[function];
            if (fn == null) return;
            lScript.Call(fn, args);
        }
    }
}
