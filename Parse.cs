using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VirtualSerial
{
    internal class Parse
    {
        private static readonly Regex matchBase16 = new Regex("[^0-9ABCDEF]");
        private static readonly Regex matchWhitespace = new Regex(@"\s+");
        Regex isNumber = new Regex(@"^\d$");
        public static byte[] ParseHexString(string str, int n)
        {
            byte[] buf = new byte[str.Length * n];
            int shunt = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i % n) == 0 && i < str.Length - 1)
                {
                    string substr = str.Substring(i, Math.Min(n, str.Length));
                    string padleft = substr.PadLeft(2, '0');
                    byte byt = Convert.ToByte(padleft, 16); ;
                    buf[shunt++] = byt;
                }
            }
            return buf;
        }

        // clean and parse a user input hex string
        // It's probably complete junk so transform it to upper
        // strip bad characters
        public static byte[] ParseBadHexStringLiteral(string str, int n)
        {
            str = str.ToUpper();
            str = matchBase16.Replace(str, "");
            str = matchWhitespace.Replace(str, "");

            byte[] buf = new byte[(str.Length / 2) + 1];
            int shunt = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i % n) == 0 && i < str.Length - 1)
                {
                    string substr = str.Substring(i, Math.Min(n, str.Length));
                    string padleft = substr.PadLeft(2, '0');
                    byte byt = Convert.ToByte(padleft, 16); ;
                    buf[shunt++] = byt;
                }
            }
            return buf;
        }
    }
}
