using System;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Cryptography
{
    public class AesManager
    {
        private const int MAX_IV_LENGTH = 16;
        private const int MAX_KEY_LENGTH = 32;

        private static byte[] GenerateValidKey(byte[] keyBytes)
        {
            byte[] ret = new byte[MAX_KEY_LENGTH];
            byte[] hash = HashManager.ToRawHash(keyBytes, HashAlgorithm.SHA256);
            Array.Copy(hash, ret, MAX_KEY_LENGTH);
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
            byte[] ret = new byte[Encrypted.Length + MAX_IV_LENGTH];
            Array.Copy(Encrypted, ret, Encrypted.Length);
            Array.Copy(AesAlgorithm.IV, 0, ret, ret.Length - MAX_IV_LENGTH, MAX_IV_LENGTH);
            return ret;
        }

        public static byte[] DecryptRaw(byte[] data, byte[] key)
        {
            AesManaged AesAlgorithm = new AesManaged()
            {
                Key = GenerateValidKey(key)
            };
            byte[] IV = new byte[MAX_IV_LENGTH];
            Array.Copy(data, data.Length - MAX_IV_LENGTH, IV, 0, MAX_IV_LENGTH);
            AesAlgorithm.IV = IV;
            byte[] RealBytes = new byte[data.Length - MAX_IV_LENGTH];
            Array.Copy(data, RealBytes, data.Length - MAX_IV_LENGTH);
            return AesAlgorithm.CreateDecryptor().TransformFinalBlock(RealBytes, 0, RealBytes.Length); ;
        }

        public static String EncryptToHex(String data, String key)
        {
            int Length = Encoding.UTF8.GetByteCount(key);
            byte[] PlainBytes = Encoding.UTF8.GetBytes(data);
            return HashManager.ToHex((EncryptRaw(PlainBytes, Encoding.UTF8.GetBytes(key))));
        }

        public static String DecryptFromHex(String data, String key)
        {
            byte[] CiPherBytes = HashManager.HexToByteArray(data);
            byte[] Encrypted = DecryptRaw(CiPherBytes, Encoding.UTF8.GetBytes(key));
            return Encoding.UTF8.GetString(Encrypted, 0, Encrypted.Length);
        }

        public static String EncryptToBase64(String data, String key, Boolean GetUrlSafe = default(Boolean))
        {
            byte[] PlainBytes = Encoding.UTF8.GetBytes(data);
            return HashManager.ToBase64(EncryptRaw(PlainBytes, Encoding.UTF8.GetBytes(key)), false, GetUrlSafe);
        }

        public static String DecryptFromBase64(String data, String key)
        {
            byte[] CiPherBytes = HashManager.Base64ToByteArray(data);
            byte[] Encrypted = DecryptRaw(CiPherBytes, Encoding.UTF8.GetBytes(key));
            return Encoding.UTF8.GetString(Encrypted, 0, Encrypted.Length);
        }

    }
}