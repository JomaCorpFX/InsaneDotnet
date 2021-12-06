using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class ByteArrayExtensions
    {
        public static void ThrowIfNullOrEmpty(this byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Invalid data. Value cannot be null or empty array.");
            }
        }

        public static void ThrowIfNull(this byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentException("Invalid data. Value cannot be null.");
            }
        }

        public static void ThrowIfEmpty(this byte[] data)
        {
            if (data.Length == 0)
            {
                throw new ArgumentException("Invalid data. Value cannot be empty array.");
            }
        }
    }
}
