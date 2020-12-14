using System;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Cryptography
{
    public class AesManager
    {
        private const int MaxIvLength = 16;
        private const int MaxKeyLength = 32;

        private static byte[] GenerateValidKey(byte[] keyBytes)
        {
            byte[] ret = new byte[MaxKeyLength];
            byte[] hash = HashManager.ToRawHash(keyBytes, HashAlgorithm.Sha512);
            Array.Copy(hash, ret, MaxKeyLength);
            return ret;
        }

        public static byte[] GenerateRandomArray(int size)
        {
            using(RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] ret = new byte[size];
                provider.GetBytes(ret);
                return ret;
            }
        }

        public static byte[] EncryptRaw(byte[] data, byte[] key)
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

        public static byte[] DecryptRaw(byte[] data, byte[] key)
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

        public static string EncryptToHex(string data, string key)
        {
            return HashManager.ToHex((EncryptRaw( HashManager.ToByteArray(data), HashManager.ToByteArray(key))));
        }

        public static string DecryptFromHex(string data, string key)
        {
            return HashManager.ToString( DecryptRaw(HashManager.FromHex(data), HashManager.ToByteArray(key)));
        }

        public static string EncryptToBase64(string data, string key)
        {
            return HashManager.ToBase64(EncryptRaw(HashManager.ToByteArray(data), HashManager.ToByteArray(key)));
        }

        public static string DecryptFromBase64(string data, string key)
        {
            return HashManager.ToString( DecryptRaw(HashManager.FromBase64(data), HashManager.ToByteArray(key)));
        }

    }
}