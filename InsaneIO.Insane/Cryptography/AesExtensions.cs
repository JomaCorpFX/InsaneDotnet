﻿using InsaneIO.Insane.Cryptography;
using Microsoft.JSInterop;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using HashAlgorithm = InsaneIO.Insane.Cryptography.HashAlgorithm;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
    public static class AesExtensions
    {
        public const int MaxIvLength = 16;
        public const int MaxKeyLength = 32;

        private static byte[] GenerateNormalizedKey(byte[] keyBytes)
        {
            return keyBytes.ComputeHash(HashAlgorithm.Sha512).Take(MaxKeyLength).ToArray();
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
                AesCbcPadding.Zeros => PaddingMode.Zeros,
                AesCbcPadding.AnsiX923 => PaddingMode.ANSIX923,
                AesCbcPadding.Pkcs7 => PaddingMode.PKCS7,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            aes.GenerateIV();
            aes.Key = GenerateNormalizedKey(key);
            using ICryptoTransform encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length).Concat(aes.IV).ToArray();
        }

        public static byte[] EncryptAesCbc(this string data, string key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return EncryptAesCbc(data.ToByteArrayUtf8(), key.ToByteArrayUtf8(), padding);
        }

        public static byte[] DecryptAesCbc(this byte[] data, byte[] key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            ValidateKey(key);
            using Aes aes = Aes.Create();
            aes.Key = GenerateNormalizedKey(key);
            aes.Padding = padding switch
            {
                AesCbcPadding.None => PaddingMode.None,
                AesCbcPadding.Zeros => PaddingMode.Zeros,
                AesCbcPadding.AnsiX923 => PaddingMode.ANSIX923,
                AesCbcPadding.Pkcs7 => PaddingMode.PKCS7,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            aes.IV = data.TakeLast(MaxIvLength).ToArray();
            byte[] bytes = data.Take(data.Length - MaxIvLength).ToArray();
            using ICryptoTransform decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(bytes, 0, bytes.Length); ;
        }

        public static byte[] DecryptAesCbc(this byte[] data, string key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return DecryptAesCbc(data, key.ToByteArrayUtf8(), padding);
        }

        public static string EncryptEncodedAesCbc(this byte[] data, byte[] key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return encoder.Encode(EncryptAesCbc(data, key, padding));
        }

        public static string EncryptEncodedAesCbc(this string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return encoder.Encode(EncryptAesCbc(data, key, padding));
        }

        public static byte[] DecryptEncodedAesCbc(this string data, byte[] key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return DecryptAesCbc(encoder.Decode(data), key, padding);
        }

        public static byte[] DecryptEncodedAesCbc(this string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return DecryptAesCbc(encoder.Decode(data), key, padding);
        }

        // █ IJSRuntime methods.
        public static async Task<byte[]> EncryptAesCbcAsync(this IJSRuntime js, byte[] data, byte[] key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (data, key, padding) => {{
    var data = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(data);
    var key  = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(key);
    var padding = Insane.AesCbcPaddingEnumExtensions.ParseInt(padding);
    var encrypted = Insane.AesExtensions.EncryptAesCbc(data, key, padding);
    data.delete();
    key.delete();
    var ret = Insane.InteropExtensions.StdVectorUint8ToJsUint8Array(encrypted);
    encrypted.delete();
    return ret;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<byte[]>($"Insane.{fxName}", data, key, padding.IntValue());
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return result;
        }

        public static async Task<byte[]> DecryptAesCbcAsync(this IJSRuntime js, byte[] data, byte[] key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (data, key, padding) => {{
    var data = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(data);
    var key  = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(key);
    var padding = Insane.AesCbcPaddingEnumExtensions.ParseInt(padding);
    var encrypted = Insane.AesExtensions.DecryptAesCbc(data, key, padding);
    data.delete();
    key.delete();
    var ret = Insane.InteropExtensions.StdVectorUint8ToJsUint8Array(encrypted);
    encrypted.delete();
    return ret;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<byte[]>($"Insane.{fxName}", data, key, padding.IntValue());
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return result;
        }

        public static async Task<byte[]> EncryptAesCbcAsync(this IJSRuntime js, string data, string key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return await EncryptAesCbcAsync(js, data.ToByteArrayUtf8(), key.ToByteArrayUtf8(), padding);
        }

        public static async Task<byte[]> DecryptAesCbcAsync(this IJSRuntime js, byte[] data, string key, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return await DecryptAesCbcAsync(js, data, key.ToByteArrayUtf8(), padding);
        }

        public static async Task<string> EncryptEncodedAesCbcAsync(this IJSRuntime js, byte[] data, byte[] key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return encoder.Encode(await EncryptAesCbcAsync(js, data, key, padding));
        }

        public static async Task<string> EncryptEncodedAesCbcAsync(this IJSRuntime js, string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return encoder.Encode(await EncryptAesCbcAsync(js, data, key, padding));
        }

        public static async Task<byte[]> DecryptEncodedAesCbcAsync(this IJSRuntime js, string data, byte[] key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return await DecryptAesCbcAsync(js, encoder.Decode(data), key, padding);
        }

        public static async Task<byte[]> DecryptEncodedAesCbcAsync(this IJSRuntime js, string data, string key, IEncoder encoder, AesCbcPadding padding = AesCbcPadding.Pkcs7)
        {
            return await DecryptAesCbcAsync(js, encoder.Decode(data), key, padding);
        }

    }
}