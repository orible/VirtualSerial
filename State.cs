using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualSerial.MainForm;

namespace VirtualSerial
{
    // represents program state
    // copies of this are sent to threads to tell them what they need to know
    public class State
    {
        public string PortID;
        public bool Connected;
        public DataBits bits;
        public Parity parity;
        public StopBits stop;
        public ReadMode buffermode;

        public int readtimeout;
        public int writetimeout;

        public bool IsReadRunning;
        public bool IsWriteRunning;
        public string UIDAttachedConsole;
        public int readBytes;
        public int writeBytes;

        public State(bool connected)
        {
            Connected = connected;
        }
    }
}
