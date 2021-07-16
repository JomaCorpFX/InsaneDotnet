using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class UtilityExtensions
    {
        public static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToStr(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        
    }
}
