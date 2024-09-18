using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static VirtualSerial.Form1;

namespace VirtualSerial
{
    [Serializable]
    public class Port
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public DataBits DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public ReadMode BufferMode { get; set; }

        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
        public string StopCode { get; set; }
    }

    [Serializable]
    public class Session
    {
        public Port port;
    }

    public class Config
    {
        public static Port DeserializePort(string buf)
        {
            return JsonSerializer.Deserialize<Port>(buf);
        }
        public static string SerializePort(Port p)
        {
            return JsonSerializer.Serialize<Port>(p);
        }
        public static string SerializeSession(Session s)
        {
            return JsonSerializer.Serialize<Session>(s);
        }
    }
}
