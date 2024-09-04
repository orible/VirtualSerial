using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualSerial.Form1;

namespace VirtualSerial
{
    public class State
    {
        public bool Connected;
        DataBits bits;
        Parity parity;
        StopBits stop;
        int read, write;

        public State(bool connected)
        {
            Connected = connected;
        }
    }
}
