using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class EncodingExtensions
    {
        public static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToStr(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] ToByteArray(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        public static string ToStr(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
    }
}
