using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
    public static class RsaExtensions
    {
        private static readonly Regex Base64ValueRegex = new Regex(Constants.Base64ValueRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));
        private static readonly Regex RsaXmlPublicKeyRegex = new Regex(Constants.RsaXmlPublicKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));
        private static readonly Regex RsaXmlPrivateKeyRegex = new Regex(Constants.RsaXmlPrivateKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));

        private static readonly Regex RsaPemPublicKeyRegex = new Regex(Constants.RsaPemPublicKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));
        private static readonly Regex RsaPemPrivateKeyRegex = new Regex(Constants.RsaPemPrivateKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));

        private static readonly Regex RsaPemRsaPublicKeyRegex = new Regex(Constants.RsaPemRsaPublicKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));
        private static readonly Regex RsaPemRsaPrivateKeyRegex = new Regex(Constants.RsaPemRsaPrivateKeyRegexPattern, RegexOptions.None, TimeSpan.FromSeconds(2));

        public static RsaKeyPair CreateRsaKeyPair(this uint keySize, RsaKeyPairEncoding encoding = RsaKeyPairEncoding.Ber)
        {
            RsaKeyPair result;
            using RSA Csp = RSA.Create((int)keySize);
            switch (encoding)
            {
                case RsaKeyPairEncoding.Xml:
                    result = new RsaKeyPair
                    {
                        PublicKey = XDocument.Parse(Csp.ToXmlString(false)).ToString(),
                        PrivateKey = XDocument.Parse(Csp.ToXmlString(true)).ToString()
                    };
                    break;
                case RsaKeyPairEncoding.Pem:
                    result = new RsaKeyPair
                    {
                        PublicKey = $"{Constants.RsaPemPublicKeyHeader}{Environment.NewLine}{Csp.ExportSubjectPublicKeyInfo().EncodeToBase64(Constants.Base64PemLineBreaksLength)}{Environment.NewLine}{Constants.RsaPemPublicKeyFooter}",
                        PrivateKey = $"{Constants.RsaPemPrivateKeyHeader}{Environment.NewLine}{Csp.ExportPkcs8PrivateKey().EncodeToBase64(Constants.Base64PemLineBreaksLength)}{Environment.NewLine}{Constants.RsaPemPrivateKeyFooter}"
                    };
                    break;
                case RsaKeyPairEncoding.PemRsa:
                    result = new RsaKeyPair
                    {
                        PublicKey = $"{Constants.RsaPemRsaPublicKeyHeader}{Environment.NewLine}{Csp.ExportSubjectPublicKeyInfo().EncodeToBase64(Constants.Base64PemLineBreaksLength)}{Environment.NewLine}{Constants.RsaPemRsaPublicKeyFooter}",
                        PrivateKey = $"{Constants.RsaPemRsaPrivateKeyHeader}{Environment.NewLine}{Csp.ExportPkcs8PrivateKey().EncodeToBase64(Constants.Base64PemLineBreaksLength)}{Environment.NewLine}{Constants.RsaPemRsaPrivateKeyFooter}"
                    };
                    break;
                case RsaKeyPairEncoding.Ber:
                    result = new RsaKeyPair
                    {
                        PublicKey = Csp.ExportSubjectPublicKeyInfo().EncodeToBase64(),
                        PrivateKey = Csp.ExportPkcs8PrivateKey().EncodeToBase64()
                    };
                    break;

                default:
                    throw new NotImplementedException(encoding.ToString());

            }
            return result;
        }

        internal static (RsaKeyEncoding Encoding, RSA Rsa) GetRsaKeyEncodingWithRSA(this string key)
        {
            key = key.Trim();
            var rsa = RSA.Create();
            if (Base64ValueRegex.IsMatch(key))
            {
                try
                {
                    rsa.ImportPkcs8PrivateKey(key.DecodeFromBase64(), out int bytesRead2);
                    return (RsaKeyEncoding.BerPrivate, rsa);
                }
                catch
                {
                    try
                    {
                        rsa.ImportSubjectPublicKeyInfo(key.DecodeFromBase64(), out int bytesRead2);
                        return (RsaKeyEncoding.BerPublic, rsa);
                    }
                    catch
                    {
                        return (RsaKeyEncoding.Unknown, rsa);
                    }
                }
            }

            try
            {

                if (RsaXmlPrivateKeyRegex.IsMatch(key))
                {
                    rsa.FromXmlString(key);
                    return (RsaKeyEncoding.XmlPrivate, rsa);
                }

                if (RsaXmlPublicKeyRegex.IsMatch(key))
                {
                    rsa.FromXmlString(key);
                    return (RsaKeyEncoding.XmlPublic, rsa);
                }

                {
                    StringBuilder pemsb = new(key);
                    if (RsaPemPublicKeyRegex.IsMatch(key))
                    {
                        pemsb.Replace(Constants.RsaPemPublicKeyHeader, string.Empty).Replace(Constants.RsaPemPublicKeyFooter, string.Empty);
                        rsa.ImportSubjectPublicKeyInfo(pemsb.ToString().Trim().DecodeFromBase64(), out int bytesRead1);
                        return (RsaKeyEncoding.PemPublic, rsa);
                    }

                    if (RsaPemPrivateKeyRegex.IsMatch(key))
                    {
                        pemsb.Replace(Constants.RsaPemPrivateKeyHeader, string.Empty).Replace(Constants.RsaPemPrivateKeyFooter, string.Empty);
                        rsa.ImportPkcs8PrivateKey(pemsb.ToString().Trim().DecodeFromBase64(), out int bytesRead1);
                        return (RsaKeyEncoding.PemPrivate, rsa);
                    }

                    if (RsaPemRsaPublicKeyRegex.IsMatch(key))
                    {
                        pemsb.Replace(Constants.RsaPemRsaPublicKeyHeader, string.Empty).Replace(Constants.RsaPemRsaPublicKeyFooter, string.Empty);
                        rsa.ImportSubjectPublicKeyInfo(pemsb.ToString().Trim().DecodeFromBase64(), out int bytesRead1);
                        return (RsaKeyEncoding.PemRsaPublic, rsa);
                    }

                    if (RsaPemRsaPrivateKeyRegex.IsMatch(key))
                    {
                        pemsb.Replace(Constants.RsaPemRsaPrivateKeyHeader, string.Empty).Replace(Constants.RsaPemRsaPrivateKeyFooter, string.Empty);
                        rsa.ImportPkcs8PrivateKey(pemsb.ToString().Trim().DecodeFromBase64(), out int bytesRead1);
                        return (RsaKeyEncoding.PemRsaPrivate, rsa);
                    }
                }
            }
            catch
            {
                return (RsaKeyEncoding.Unknown, rsa);
            }

            return (RsaKeyEncoding.Unknown, rsa);
        }

        public static RsaKeyEncoding GetRsaKeyEncoding(this string key)
        {
            return GetRsaKeyEncodingWithRSA(key).Encoding;
        }


        public static bool ValidateRsaPublicKey(this string publicKey)
        {
            RsaKeyEncoding encoding = GetRsaKeyEncoding(publicKey);
            if (encoding == RsaKeyEncoding.XmlPublic ||
                encoding == RsaKeyEncoding.PemPublic ||
                encoding == RsaKeyEncoding.PemRsaPublic ||
                encoding == RsaKeyEncoding.BerPublic)
            {
                return true;
            }
            return false;
        }

        public static bool ValidateRsaPrivateKey(this string privateKey)
        {
            RsaKeyEncoding encoding = GetRsaKeyEncoding(privateKey);
            if (encoding == RsaKeyEncoding.XmlPrivate ||
                encoding == RsaKeyEncoding.PemPrivate ||
                encoding == RsaKeyEncoding.PemRsaPrivate ||
                encoding == RsaKeyEncoding.BerPrivate)
            {
                return true;
            }
            return false;
        }



        private static RSA ParsePublicKey(string publicKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty public key.");
            var result = GetRsaKeyEncodingWithRSA(publicKey);
            if (result.Encoding == RsaKeyEncoding.XmlPublic ||
            result.Encoding == RsaKeyEncoding.PemPublic ||
            result.Encoding == RsaKeyEncoding.PemRsaPublic ||
            result.Encoding == RsaKeyEncoding.BerPublic)
            {
                return result.Rsa;
            }
            throw new ArgumentException("Unable to parse private key.");
        }

        private static RSA ParsePrivateKey(string privateKey)
        {
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentException("Invalid null or empty private key.");
            var result = GetRsaKeyEncodingWithRSA(privateKey);
            if (result.Encoding == RsaKeyEncoding.XmlPrivate ||
            result.Encoding == RsaKeyEncoding.PemPrivate ||
            result.Encoding == RsaKeyEncoding.PemRsaPrivate ||
            result.Encoding == RsaKeyEncoding.BerPrivate)
            {
                return result.Rsa;
            }
            throw new ArgumentException("Unable to parse private key.");
        }

        public static byte[] EncryptRsa(this byte[] data, string publicKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            using RSA rsa = ParsePublicKey(publicKey);
            RSAEncryptionPadding rsaPadding = padding switch
            {
                RsaPadding.Pkcs1 => RSAEncryptionPadding.Pkcs1,
                RsaPadding.OaepSha1 => RSAEncryptionPadding.OaepSHA1,
                RsaPadding.OaepSha256 => RSAEncryptionPadding.OaepSHA256,
                RsaPadding.OaepSha384 => RSAEncryptionPadding.OaepSHA384,
                RsaPadding.OaepSha512 => RSAEncryptionPadding.OaepSHA512,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            return rsa.Encrypt(data, rsaPadding);
        }

        public static byte[] EncryptRsa(this string data, string publicKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return EncryptRsa(data.ToByteArrayUtf8(), publicKey, padding);
        }

        public static string EncryptEncodedRsa(this byte[] data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return encoder.Encode(EncryptRsa(data, publicKey, padding));
        }

        public static string EncryptEncodedRsa(this string data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return encoder.Encode(EncryptRsa(data, publicKey, padding));
        }


        public static byte[] DecryptRsa(this byte[] data, string privateKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            using RSA rsa = ParsePrivateKey(privateKey);
            RSAEncryptionPadding rsaPadding = padding switch
            {
                RsaPadding.Pkcs1 => RSAEncryptionPadding.Pkcs1,
                RsaPadding.OaepSha1 => RSAEncryptionPadding.OaepSHA1,
                RsaPadding.OaepSha256 => RSAEncryptionPadding.OaepSHA256,
                RsaPadding.OaepSha384 => RSAEncryptionPadding.OaepSHA384,
                RsaPadding.OaepSha512 => RSAEncryptionPadding.OaepSHA512,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            return rsa.Decrypt(data, rsaPadding);
        }

        public static byte[] DecryptEncodedRsa(this string data, string privateKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return DecryptRsa(encoder.Decode(data), privateKey, padding);
        }

        // █ IJSRuntime methods.

        public static async Task<RsaKeyPair> CreateRsaKeyPairAsync(this IJSRuntime js, uint keySize, RsaKeyPairEncoding encoding = RsaKeyPairEncoding.Ber)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (keySize, encoding) => {{
    var keypair = Insane.RsaExtensions.CreateRsaKeyPair(keySize, Insane.RsaKeyEncodingEnumExtensions.ParseInt(encoding));
    var result = keypair.Serialize(true);
    keypair.delete();
    return result;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<string>($"Insane.{fxName}", keySize, encoding.IntValue());
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return JsonSerializer.Deserialize<RsaKeyPair>(result)!;
        }

        public static async Task<bool> ValidateRsaPublicKeyAsync(this IJSRuntime js, string publicKey)
        {
            return await js.InvokeAsync<bool>("Insane.RsaExtensions.ValidateRsaPublicKey", publicKey);
        }

        public static async Task<bool> ValidateRsaPrivateKeyAsync(this IJSRuntime js, string privateKey)
        {
            return await js.InvokeAsync<bool>("Insane.RsaExtensions.ValidateRsaPrivateKey", privateKey);
        }

        public static async Task<RsaKeyPairEncoding> GetRsaKeyEncoding(this IJSRuntime js, string key)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (key) => {{
    var encoding = Insane.RsaExtensions.GetRsaKeyEncoding(key);
    return encoding.value;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<RsaKeyPairEncoding>($"Insane.{fxName}", key);
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return result;
        }


        public static async Task<byte[]> EncryptRsaAsync(this IJSRuntime js, byte[] data, string publicKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (data, publicKey, padding) => {{
    var data = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(data);
    var padding = Insane.RsaPaddingEnumExtensions.ParseInt(padding);
    var encrypted = Insane.RsaExtensions.EncryptRsa(data, publicKey, padding);
    data.delete();
    var ret = Insane.InteropExtensions.StdVectorUint8ToJsUint8Array(encrypted);
    encrypted.delete();
    return ret;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<byte[]>($"Insane.{fxName}", data, publicKey, padding.IntValue());
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return result;
        }


        public static async Task<byte[]> EncryptRsaAsync(this IJSRuntime js, string data, string publicKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return await EncryptRsaAsync(js, data.ToByteArrayUtf8(), publicKey, padding);
        }

        public static async Task<string> EncryptEncodedRsaAsync(this IJSRuntime js, byte[] data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return encoder.Encode(await EncryptRsaAsync(js, data, publicKey, padding));
        }

        public static async Task<string> EncryptEncodedRsaAsync(this IJSRuntime js, string data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return encoder.Encode(await EncryptRsaAsync(js, data, publicKey, padding));
        }


        public static async Task<byte[]> DecryptRsaAsync(this IJSRuntime js, byte[] data, string privateKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            string fxName = "Insane_" + HexEncoder.DefaultInstance.Encode(RandomExtensions.NextBytes(16));
            string jscode = @$"
Insane.{fxName} = (data, privateKey, padding) => {{
    var data = Insane.InteropExtensions.JsUint8ArrayToStdVectorUint8(data);
    var padding = Insane.RsaPaddingEnumExtensions.ParseInt(padding);
    var encrypted = Insane.RsaExtensions.DecryptRsa(data, privateKey, padding);
    data.delete();
    var ret = Insane.InteropExtensions.StdVectorUint8ToJsUint8Array(encrypted);
    encrypted.delete();
    return ret;
}};
";
            await js.InvokeAsync<object>("eval", jscode);
            var result = await js.InvokeAsync<byte[]>($"Insane.{fxName}", data, privateKey, padding.IntValue());
            await js.InvokeAsync<object>("eval", $"delete Insane.{fxName};");
            return result;
        }

        public static async Task<byte[]> DecryptEncodedRsaAsync(this IJSRuntime js, string data, string privateKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return await DecryptRsaAsync(js, encoder.Decode(data), privateKey, padding);
        }
    }
}