using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Insane.Interop;


namespace Insane.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public static class RandomManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            byte[] intBytes = new byte[4];
            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(intBytes);
            }
            return BitConverter.ToInt32(intBytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException("Min value is greater or equals than Max value.");
            }
            byte[] intBytes = new byte[4];
            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(intBytes);
            }
            return  min +  Math.Abs(BitConverter.ToInt32(intBytes, 0)) % (max - min + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] Next(int size)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] ret = new byte[size];
                provider.GetBytes(ret);
                return ret;
            }
        }
    }
}
