using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSerial
{
    public class Message
    {
        public enum MessageCode { STOP = 1, WRITE = 2, READ = 3, NULL = 0 };
        public enum SendAsEncoding { ASCII_utf8, ASCII, ASCII_UTF7 };
        public MessageCode Code = MessageCode.NULL;
        public string UserRepresentation;
        public string SBuf;
        public byte[] Buf;
        public SendAsEncoding Encoding;
        public DateTime time = DateTime.Now;
        public Message(MessageCode code)
        {
            Code = code;
        }
        public Message(MessageCode code, byte[] inBuf)
        {
            Code = code;
        }
        public Message(string str, SendAsEncoding encoding)
        {
            SBuf = str;
        }
        public Message(byte[] inBuf)
        {
            Buf = inBuf;
        }
        public Message(MessageCode code, byte[] inBuf, SendAsEncoding encoding)
        {
            Buf = inBuf;
            encoding = encoding;
        }
        public Message(string userRepresentation, byte[] inBuf, SendAsEncoding encoding)
        {
            Buf = inBuf;
            UserRepresentation = userRepresentation;
            Encoding = encoding;
        }
    }
}
