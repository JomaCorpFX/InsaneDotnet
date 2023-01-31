using InsaneIO.Insane.Cryptography;
using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
    public static class AesExtensions
    {
        public const int MaxIvLength = 16;
        public const int MaxKeyLength = 32;

        private static byte[] GenerateNormalizedKey(byte[] keyBytes)
        {
            return keyBytes.ToHash().Take(MaxKeyLength).ToArray();
        }

        private static void ValidateKey(byte[] key)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (key.Length < 8) throw new ArgumentException("Key must be at least 8 bytes.");
        }

        public static byte[] EncryptAesCbc(this byte[] data, byte[] key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            ValidateKey(key);
            using var aes = Aes.Create();
            aes.Padding = padding switch
            {
                AesCbcPadding.None => PaddingMode.None,
                AesCbcPadding.AnsiX923 => PaddingMode.ANSIX923,
                AesCbcPadding.Pkcs7 => PaddingMode.PKCS7,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            aes.GenerateIV();
            aes.Key = GenerateNormalizedKey(key);
            using ICryptoTransform encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length).Concat(aes.IV).ToArray();
        }

        public static byte[] DecryptAesCbc(this byte[] data, byte[] key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            ValidateKey(key);
            using Aes aes = Aes.Create();
            aes.Key = GenerateNormalizedKey(key);
            aes.Padding = padding switch
            {
                AesCbcPadding.None => PaddingMode.None,
                AesCbcPadding.AnsiX923 => PaddingMode.ANSIX923,
                AesCbcPadding.Pkcs7 => PaddingMode.PKCS7,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            aes.IV = data.TakeLast(MaxIvLength).ToArray();
            byte[] bytes = data.Take(data.Length - MaxIvLength).ToArray();
            using ICryptoTransform decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(bytes, 0, bytes.Length); ;
        }

        public static string EncryptAesCbc(this string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return encoder.Encode(EncryptAesCbc(data.ToByteArrayUtf8(), key.ToByteArrayUtf8(), padding));
        }

        public static string DecryptAesCbc(this string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return DecryptAesCbc(encoder.Decode(data), key.ToByteArrayUtf8(), padding).ToStringUtf8();
        }


    }
}