using Insane.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Cryptography
{
    public static class AesExtensions
    {
        private const int MaxIvLength = 16;
        private const int MaxKeyLength = 32;

        private static byte[] GenerateValidKey(byte[] keyBytes)
        {
            byte[] ret = new byte[MaxKeyLength];
            byte[] hash = HashExtensions.ToRawHash(keyBytes, HashAlgorithm.Sha512);
            Array.Copy(hash, ret, MaxKeyLength);
            return ret;
        }

        public static byte[] EncryptAes(this byte[] data, byte[] key)
        {
            AesManaged AesAlgorithm = new AesManaged()
            {
                Key = GenerateValidKey(key)
            };
            AesAlgorithm.GenerateIV();
            var Encrypted = AesAlgorithm.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            byte[] ret = new byte[Encrypted.Length + MaxIvLength];
            Array.Copy(Encrypted, ret, Encrypted.Length);
            Array.Copy(AesAlgorithm.IV, 0, ret, ret.Length - MaxIvLength, MaxIvLength);
            return ret;
        }

        public static byte[] DecryptAes(this byte[] data, byte[] key)
        {
            AesManaged AesAlgorithm = new AesManaged()
            {
                Key = GenerateValidKey(key)
            };
            byte[] IV = new byte[MaxIvLength];
            Array.Copy(data, data.Length - MaxIvLength, IV, 0, MaxIvLength);
            AesAlgorithm.IV = IV;
            byte[] RealBytes = new byte[data.Length - MaxIvLength];
            Array.Copy(data, RealBytes, data.Length - MaxIvLength);
            return AesAlgorithm.CreateDecryptor().TransformFinalBlock(RealBytes, 0, RealBytes.Length); ;
        }

        public static string EncryptAes(this string data, string key, IEncoder encoder)
        {
            return encoder.Encode(EncryptAes(data.ToByteArray(), key.ToByteArray()));
        }

        public static string DecryptAes(this string data, string key, IEncoder encoder)
        {
            return DecryptAes(encoder.Decode(data), key.ToByteArray()).ToStr();
        }

    }
}