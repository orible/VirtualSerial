using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSerial
{
    class StopBuffer
    {
        MemoryStream buffer = new MemoryStream();
        long ReadPosition;
        long WritePosition;

        // scan buffer from last read position and see if we can detect a OK buffer to eat
        public int GetMessage(byte[] stopcode, out byte[] outbuf)
        {
            int stop = -1;
            byte[] _buffer = buffer.GetBuffer();
            // search for stopcode
            for (int i = (int)ReadPosition; i < WritePosition; i++)
            {
                if (_buffer[i] == stopcode[0])
                {
                    stop = i;
                }
            }

            int bufsz = stop - (int)ReadPosition;

            if (stop == -1 || bufsz < 1)
            {
                outbuf = null;
                return -1;
            }

            byte[] tbuf = new byte[bufsz];

            buffer.Position = ReadPosition;
            buffer.Read(tbuf, 0, bufsz);

            // for whatever reason stop - 1 fixes the read clipping the beginning of lines and missing messages
            ReadPosition = stop;

            outbuf = tbuf;
            return bufsz;
        }

        // add data and update position
        public void Add(byte[]buf, int size)
        {
            buffer.Write(buf, 0, size);
            WritePosition = buffer.Position;
        }
    }
}
