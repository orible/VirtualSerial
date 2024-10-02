using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualSerial.Form1;

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

        public string UIDAttachedConsole;

        public State(bool connected)
        {
            Connected = connected;
        }
    }
}
