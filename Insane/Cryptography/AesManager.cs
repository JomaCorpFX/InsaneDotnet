﻿using System;
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

        public static byte[] EncryptRaw(byte[] PlainBytes, byte[] Key)
        {
            AesManaged AesAlgorithm = new AesManaged()
            {
                Key = GenerateValidKey(Key)
            };
            AesAlgorithm.GenerateIV();
            var Encrypted = AesAlgorithm.CreateEncryptor().TransformFinalBlock(PlainBytes, 0, PlainBytes.Length);
            byte[] ret = new byte[Encrypted.Length + MAX_IV_LENGTH];
            Array.Copy(Encrypted, ret, Encrypted.Length);
            Array.Copy(AesAlgorithm.IV, 0, ret, ret.Length - MAX_IV_LENGTH, MAX_IV_LENGTH);
            return ret;
        }

        public static byte[] DecryptRaw(byte[] CipherBytes, byte[] Key)
        {
            AesManaged AesAlgorithm = new AesManaged()
            {
                Key = GenerateValidKey(Key)
            };
            byte[] IV = new byte[MAX_IV_LENGTH];
            Array.Copy(CipherBytes, CipherBytes.Length - MAX_IV_LENGTH, IV, 0, MAX_IV_LENGTH);
            AesAlgorithm.IV = IV;
            byte[] RealBytes = new byte[CipherBytes.Length - MAX_IV_LENGTH];
            Array.Copy(CipherBytes, RealBytes, CipherBytes.Length - MAX_IV_LENGTH);
            return AesAlgorithm.CreateDecryptor().TransformFinalBlock(RealBytes, 0, RealBytes.Length); ;
        }

        public static String EncryptToHex(String Plaintext, String Key)
        {
            int Length = Encoding.UTF8.GetByteCount(Key);
            byte[] PlainBytes = Encoding.UTF8.GetBytes(Plaintext);
            return HashManager.ToHex((EncryptRaw(PlainBytes, Encoding.UTF8.GetBytes(Key))));
        }

        public static String DecryptFromHex(String CipherText, String Key)
        {
            byte[] CiPherBytes = HashManager.HexToByteArray(CipherText);
            byte[] Encrypted = DecryptRaw(CiPherBytes, Encoding.UTF8.GetBytes(Key));
            return Encoding.UTF8.GetString(Encrypted, 0, Encrypted.Length);
        }

        public static String EncryptToBase64(String Plaintext, String Key, Boolean GetUrlSafe = default(Boolean))
        {
            byte[] PlainBytes = Encoding.UTF8.GetBytes(Plaintext);
            return HashManager.ToBase64(EncryptRaw(PlainBytes, Encoding.UTF8.GetBytes(Key)), false, GetUrlSafe);
        }

        public static String DecryptFromBase64(String CipherText, String Key)
        {
            byte[] CiPherBytes = HashManager.Base64ToByteArray(CipherText);
            byte[] Encrypted = DecryptRaw(CiPherBytes, Encoding.UTF8.GetBytes(Key));
            return Encoding.UTF8.GetString(Encrypted, 0, Encrypted.Length);
        }

    }
}