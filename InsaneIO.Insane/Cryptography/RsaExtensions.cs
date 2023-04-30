using Microsoft.VisualBasic;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
    public static class RsaExtensions
    {

        private const string RsaPemPrivateKeyInitialString = "-----BEGIN PRIVATE KEY-----";
        private const string RsaPemPrivateKeyFinalString = "-----END PRIVATE KEY-----";
        private const string RsaPemPublicKeyInitialString = "-----BEGIN PUBLIC KEY-----";
        private const string RsaPemPublicKeyFinalString = "-----END PUBLIC KEY-----";
        private const string PemPublicAndPrivateKeyPattern = "^(?:(-----BEGIN PUBLIC KEY-----)(?:\\r|\\n|\\r\\n)((?:(?:(?:[A-Za-z0-9+\\/]{4}){16}(?:\\r|\\n|\\r\\n))+)(?:(?:[A-Za-z0-9+\\/]{4}){0,15})(?:(?:[A-Za-z0-9+\\/]{4}|[A-Za-z0-9+\\/]{2}==|[A-Za-z0-9+\\/]{3}=)))(?:\\r|\\n|\\r\\n)(-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----)(?:\\r|\\n|\\r\\n)((?:(?:(?:[A-Za-z0-9+\\/]{4}){16}(?:\\r|\\n|\\r\\n))+)(?:(?:[A-Za-z0-9+\\/]{4}){0,15})(?:(?:[A-Za-z0-9+\\/]{4}|[A-Za-z0-9+\\/]{2}==|[A-Za-z0-9+\\/]{3}=)))(?:\\r|\\n|\\r\\n)(-----END PRIVATE KEY-----))$"; // public https://regex101.com/r/tdV9cC/1 private https://regex101.com/r/tdV9cC/2
        private const string XmlPublicAndPrivateKeyPattern = "(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()|\\s*<\\s*P\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*P\\s*>()|\\s*<\\s*Q\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Q\\s*>()|\\s*<\\s*DP\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DP\\s*>()|\\s*<\\s*DQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DQ\\s*>()|\\s*<\\s*InverseQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*InverseQ\\s*>()|\\s*<\\s*D\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*D\\s*>()){8}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()){2}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\11\\12)"; //https://regex101.com/r/fQV2VN/4  (\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()|\s*<\s*P\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*P\s*>()|\s*<\s*Q\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Q\s*>()|\s*<\s*DP\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DP\s*>()|\s*<\s*DQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DQ\s*>()|\s*<\s*InverseQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*InverseQ\s*>()|\s*<\s*D\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*D\s*>()){8}\s*<\/\s*RSAKeyValue\s*>\s*\2\3\4\5\6\7\8\9)|(\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()){2}\s*<\/\s*RSAKeyValue\s*>\s*\11\12)
        private const string Base64ValuePattern = "^(?:(?:[A-Za-z0-9+\\/]{4})*)(?:[A-Za-z0-9+\\/]{2}==|[A-Za-z0-9+\\/]{3}=)?$";


        public static RsaKeyPair CreateRsaKeyPair(this uint keySize, RsaKeyEncoding encoding = RsaKeyEncoding.Ber)
        {
            RsaKeyPair result;
            using RSA Csp = RSA.Create((int)keySize);
            switch (encoding)
            {
                case RsaKeyEncoding.Xml:
                    result = new RsaKeyPair
                    {
                        PublicKey = XDocument.Parse(Csp.ToXmlString(false)).ToString(),
                        PrivateKey = XDocument.Parse(Csp.ToXmlString(true)).ToString()
                    };
                    break;
                case RsaKeyEncoding.Pem:
                    result = new RsaKeyPair
                    {
                        PublicKey = $"{RsaPemPublicKeyInitialString}{Environment.NewLine}{Csp.ExportSubjectPublicKeyInfo().ToBase64(HashExtensions.PemLineBreaksLength)}{Environment.NewLine}{RsaPemPublicKeyFinalString}",
                        PrivateKey = $"{RsaPemPrivateKeyInitialString}{Environment.NewLine}{Csp.ExportPkcs8PrivateKey().ToBase64(HashExtensions.PemLineBreaksLength)}{Environment.NewLine}{RsaPemPrivateKeyFinalString}"
                    };
                    break;
                default:
                    result = new RsaKeyPair
                    {
                        PublicKey = Csp.ExportSubjectPublicKeyInfo().ToBase64(),
                        PrivateKey = Csp.ExportPkcs8PrivateKey().ToBase64()
                    };
                    break;
            }
            return result;
        }

        public static bool ValidateRsaPublicKey(this string publicKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(publicKey)) return false;
                using RSA csp = RSA.Create();
                ParsePublicKey(csp, publicKey);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateRsaPrivateKey(this string privateKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(privateKey)) return false;
                using RSA csp = RSA.Create();
                ParsePrivateKey(csp, privateKey);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static RsaKeyEncoding GetRsaKeyEncoding(string key)
        {
            if (Regex.IsMatch(key, Base64ValuePattern, RegexOptions.Multiline, TimeSpan.FromSeconds(2)))
            {
                return RsaKeyEncoding.Ber;
            }
            if (Regex.IsMatch(key, XmlPublicAndPrivateKeyPattern, RegexOptions.Multiline, TimeSpan.FromSeconds(2)))
            {
                return RsaKeyEncoding.Xml;
            }
            if (Regex.IsMatch(key, PemPublicAndPrivateKeyPattern, RegexOptions.Multiline, TimeSpan.FromSeconds(2)))
            {
                return RsaKeyEncoding.Pem;
            }
            throw new ArgumentException("Invalid key encoding.");
        }

        private static void ParsePublicKey(RSA rsa, string publicKey)
        {
            publicKey = publicKey.Trim();
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty private key.");
            try
            {
                switch (GetRsaKeyEncoding(publicKey))
                {
                    case RsaKeyEncoding.Xml:
                        rsa.FromXmlString(publicKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        StringBuilder pemsb = new(publicKey);
                        pemsb.Replace(RsaPemPublicKeyInitialString, string.Empty).Replace(RsaPemPublicKeyFinalString, string.Empty);
                        rsa.ImportSubjectPublicKeyInfo(pemsb.ToString().Trim().FromBase64(), out int bytesRead1);
                        break;
                    default:
                        rsa.ImportSubjectPublicKeyInfo(publicKey.FromBase64(), out int bytesRead2);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to parse public key.", ex);
            }
        }

        private static void ParsePrivateKey(RSA rsa, string privateKey)
        {
            privateKey = privateKey.Trim();
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentException("Invalid null or empty private key.");
            try
            {
                switch (GetRsaKeyEncoding(privateKey))
                {
                    case RsaKeyEncoding.Xml:
                        rsa.FromXmlString(privateKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        StringBuilder pemsb = new(privateKey);
                        pemsb.Replace(RsaPemPrivateKeyInitialString, string.Empty).Replace(RsaPemPrivateKeyFinalString, string.Empty);
                        rsa.ImportPkcs8PrivateKey(pemsb.ToString().Trim().FromBase64(), out int bytesRead1);
                        break;
                    default:
                        rsa.ImportPkcs8PrivateKey(privateKey.FromBase64(), out int bytesRead2);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to parse private key.", ex);
            }
        }

        public static byte[] EncryptRsa(this byte[] data, string publicKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            using RSA rsa = RSA.Create();
            ParsePublicKey(rsa, publicKey);
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

        public static string EncryptRsa(this string data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return encoder.Encode(EncryptRsa(data.ToByteArrayUtf8(), publicKey, padding));
        }

        public static byte[] DecryptRsa(this byte[] data, string privateKey, RsaPadding padding = RsaPadding.OaepSha256)
        {
            using RSA rsa = RSA.Create();
            ParsePrivateKey(rsa, privateKey);
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

        public static string DecryptRsa(this string data, string privateKey, IEncoder encoder, RsaPadding padding = RsaPadding.OaepSha256)
        {
            return DecryptRsa(encoder.Decode(data), privateKey, padding).ToStringFromUtf8();
        }
    }
}