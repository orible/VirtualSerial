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
    class VM
    {
        // data
        private string bufferedScript;
        private Script lScript;

        // main function address
        private DynValue fnMain;
        
        // callback timers
        Dictionary<string, Timer> timers;
        
        // in and out messages
        BlockingCollection<Message> _in;
        BlockingCollection<Message> _out;
        class ThreadCtx
        {

        }
        void _Tick()
        {
            foreach(var e in timers)
            {
                // tick all timers
                e.Value.Func.Function.Call();
            }
        }
        void _Thread(object ctx)
        {
            _in.Take();
            _out.Take();
            _Tick();
        }

        void _StartThread()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(_Thread));
        }

        public enum VMEventCodeEnum
        {
            PRINT = 1,
            LOG = 2,
            ERROR = 3,
            FAULT = 4,
            EXCEPTION = 5,
            FNCONNECT = 10,
            FNDISCONNECT = 11,
            FNWRITEFILE = 12,
        }
        public class Timer
        {
            public int Delay;
            public int Repeat;
            public int LastCall;
            public DynValue Func;
        }
        public class VMEvent
        {
            public VMEventCodeEnum Code;
            public object Data;
            public VMEvent(VMEventCodeEnum code, object Data) { this.Code = code; this.Data = Data; }
        };
        public delegate void VMEventHandler(object sender, VMEvent e);
        public event VMEventHandler EventHandler;

        public void SetScript(string buffer)
        {
            bufferedScript = buffer;
        }
        public void Run()
        {
            fnMain.Function.Call();
        }
        public void Compile()
        {
            Script vmScript = new Script();
            DynValue _fnMain = vmScript.LoadString(bufferedScript);
            vmScript.Globals["SEND"] = (Func<string, int>)lfuncSend;
            vmScript.Globals["TIMER"] = (Func<string, int, int, DynValue, int>)lfuncTimer;
            vmScript.Globals["CONNECT"] = (Func<string, string, int, string, float, int, int, int, int>)lfuncConnect;
            vmScript.Globals["DISCONNECT"] = (Func<int>)lfuncDisconnect;
            vmScript.Globals["print"] = (Func<string, int>)lfuncPrint;
            vmScript.Globals["debug"] = (Func<string, int>)lfuncPrint;
            vmScript.Globals["WITEFILE"] = (Func<string, MoonSharp.Interpreter.Table, int>)lfuncWriteFile;
            fnMain = _fnMain;
            lScript = vmScript;
        }

        int lfuncConnect(
            string port,
            string buffermode,
            int baud,
            string parity,
            float stop,
            int data,
            int readtimeout,
            int writetimeout)
        {
            return -1;
        }
        int lfuncDisconnect()
        {
            return -1;
        }

        int lfuncTimer(string name, int delay, int repeat, DynValue value)
        {
            value.Function.Call();
            return -1;
        }

        int lfuncWriteFile(string dir, MoonSharp.Interpreter.Table list)
        {
            string path = $"./output/{dir}";
            string dirpath = Path.GetFullPath(Path.GetDirectoryName(path));
            System.IO.Directory.CreateDirectory(dirpath);
            File.AppendAllLines(path, new string[] { list.TableToJson() });
            return 1;
        }
        int lfuncPrint(string str)
        {
            EventHandler.Invoke(this, new VMEvent(VMEventCodeEnum.PRINT, str));
            return 1;
        }
        int lfuncSend(string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            this._out.Add(new Message(Message.MessageCode.NULL, buf, Message.SendAsEncoding.ASCII));
            return 0;
        }

        public void CallOnMessage(object sender, MessageEventArgs args)
        {
            if (lScript == null) return;
            if (args.Data == null || args.Data.Buf == null) return;
            string data = Encoding.ASCII.GetString(args.Data.Buf);
            var fn = lScript.Globals["OnReceive"];
            if (fn == null) return;
            lScript.Call(fn, data);
        }
    }
}
