using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenCommonDLL
{
    public static class Decoding
    {
        public static string DecodeFromUtf8(this string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }
            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        public static string Utf8ToUtf16(string utf8String)
        {
            // Get UTF-8 bytes and remove binary 0 bytes (filler)
            List<byte> utf8Bytes = new List<byte>(utf8String.Length);
            foreach (byte utf8Byte in utf8String)
            {
                // Remove binary 0 bytes (filler)
                if (utf8Byte > 0)
                {
                    utf8Bytes.Add(utf8Byte);
                }
            }

            // Convert UTF-8 bytes to UTF-16 string
            return Encoding.UTF8.GetString(utf8Bytes.ToArray());
        }
    }
}
