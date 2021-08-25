using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Insane.Cryptography;

namespace Insane.Extensions
{
    public static class RsaExtensions
    {

        private const string RsaPemPrivateKeyInitialString = "-----BEGIN PRIVATE KEY-----";
        private const string RsaPemPrivateKeyFinalString = "-----END PRIVATE KEY-----";
        private const string RsaPemPublicKeyInitialString = "-----BEGIN PUBLIC KEY-----";
        private const string RsaPemPublicKeyFinalString = "-----END PUBLIC KEY-----";

        private const string PemPublicAndPrivateKeyPattern = "(-----BEGIN PUBLIC KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PRIVATE KEY-----)"; //https://regex101.com/r/mGnr7I/1  (-----BEGIN PUBLIC KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PRIVATE KEY-----)
        private const string JsonPublicAndPrivateKeyPattern = "(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"P\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Q\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DP\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"InverseQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"D\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\\s*\\}\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(12)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(11)|,)()){2}\\s*\\}\\s*\\11\\12)"; //https://regex101.com/r/FNRFqV/2  (\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"P"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Q"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DP"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"InverseQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"D"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\s*\}\s*\2\3\4\5\6\7\8\9)|(\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(12)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(11)|,)()){2}\s*\}\s*\11\12)
        private const string XmlPublicAndPrivateKeyPattern = "(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()|\\s*<\\s*P\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*P\\s*>()|\\s*<\\s*Q\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Q\\s*>()|\\s*<\\s*DP\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DP\\s*>()|\\s*<\\s*DQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DQ\\s*>()|\\s*<\\s*InverseQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*InverseQ\\s*>()|\\s*<\\s*D\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*D\\s*>()){8}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()){2}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\11\\12)"; //https://regex101.com/r/fQV2VN/4  (\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()|\s*<\s*P\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*P\s*>()|\s*<\s*Q\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Q\s*>()|\s*<\s*DP\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DP\s*>()|\s*<\s*DQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DQ\s*>()|\s*<\s*InverseQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*InverseQ\s*>()|\s*<\s*D\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*D\s*>()){8}\s*<\/\s*RSAKeyValue\s*>\s*\2\3\4\5\6\7\8\9)|(\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()){2}\s*<\/\s*RSAKeyValue\s*>\s*\11\12)


        public static RsaKeyPair CreateRsaKeyPair(this uint keySize, RsaKeyEncoding encoding = RsaKeyEncoding.Ber, bool indent = true)
        {
            RsaKeyPair result;
            using (RSACryptoServiceProvider Csp = new RSACryptoServiceProvider((int)keySize))
            {
                switch (encoding)
                {
                    case RsaKeyEncoding.Xml:
                        result = new RsaKeyPair(
                        publicKey: XDocument.Parse(Csp.ToXmlString(false)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting),
                        privateKey: XDocument.Parse(Csp.ToXmlString(true)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting)
                        );
                        break;
                    case RsaKeyEncoding.Json:
                        RSAParameters parameters = Csp.ExportParameters(true);
                        var pubKey = new
                        {
                            Modulus = (parameters.Modulus!).ToBase64(),
                            Exponent = (parameters.Exponent!).ToBase64(),
                        };
                        var privKey = new
                        {
                            Modulus = (parameters.Modulus!).ToBase64(),
                            Exponent = (parameters.Exponent!).ToBase64(),
                            P = (parameters.P!).ToBase64(),
                            Q = (parameters.Q!).ToBase64(),
                            DP = (parameters.DP!).ToBase64(),
                            DQ = (parameters.DQ!).ToBase64(),
                            InverseQ = (parameters.InverseQ!).ToBase64(),
                            D = (parameters.D!).ToBase64()
                        };
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = indent
                        };
                        result = new RsaKeyPair(JsonSerializer.Serialize(pubKey, options), JsonSerializer.Serialize(privKey, options));
                        break;
                    case RsaKeyEncoding.Pem:
                        result = new RsaKeyPair($"{RsaPemPublicKeyInitialString}{Environment.NewLine}{Csp.ExportSubjectPublicKeyInfo().ToBase64(HashExtensions.PemLineBreaksLength)}{Environment.NewLine}{RsaPemPublicKeyFinalString}",
                                              $"{RsaPemPrivateKeyInitialString}{Environment.NewLine}{Csp.ExportPkcs8PrivateKey().ToBase64(HashExtensions.PemLineBreaksLength)}{Environment.NewLine}{RsaPemPrivateKeyFinalString}");
                        break;
                    default:
                        result = new RsaKeyPair(publicKey: Csp.ExportSubjectPublicKeyInfo().ToBase64(), privateKey: Csp.ExportPkcs8PrivateKey().ToBase64());
                        break;
                }
                ValidateRsaPublicKey(result.PublicKey);
                ValidateRsaPrivateKey(result.PrivateKey);
                return result;
            }
        }

        public static void ValidateRsaPublicKey(this string publicKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty public key.");
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePublicKey(csp, publicKey);
            }
        }

        public static void ValidateRsaPrivateKey(this string privateKey)
        {
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentException("Invalid null or empty private key.");

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePrivateKey(csp, privateKey);
            }
        }

        public static RsaKeyEncoding GetKeyEncoding(string key)
        {
            if (Regex.IsMatch(key, JsonPublicAndPrivateKeyPattern, RegexOptions.Multiline, TimeSpan.FromSeconds(2)))
            {
                return RsaKeyEncoding.Json;
            }

            if (Regex.IsMatch(key, XmlPublicAndPrivateKeyPattern))
            {
                return RsaKeyEncoding.Xml;
            }
            if (Regex.IsMatch(key, PemPublicAndPrivateKeyPattern, RegexOptions.Multiline, TimeSpan.FromSeconds(2)))
            {
                return RsaKeyEncoding.Pem;
            }

            return RsaKeyEncoding.Ber;
        }

        private static void ParsePublicKey(RSACryptoServiceProvider csp, string publicKey)
        {
            publicKey = publicKey.Trim();
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty private key.");
            try
            {
                switch (GetKeyEncoding(publicKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonDocument jsonDoc = JsonDocument.Parse(publicKey, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Disallow, MaxDepth = 1 });
                        JsonElement jsonRoot = jsonDoc.RootElement;
                        var parameters = new RSAParameters();
                        parameters.Modulus = jsonRoot.GetProperty(nameof(parameters.Modulus)).GetString()!.ToString().FromBase64();
                        parameters.Exponent = jsonRoot.GetProperty(nameof(parameters.Exponent)).GetString()!.ToString().FromBase64();
                        csp.ImportParameters(parameters);
                        break;
                    case RsaKeyEncoding.Xml:
                        csp.FromXmlString(publicKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        csp.ImportSubjectPublicKeyInfo(publicKey.Replace(RsaPemPublicKeyInitialString, string.Empty).Replace(RsaPemPublicKeyFinalString, string.Empty).Trim().FromBase64(), out int bytesRead1);
                        break;
                    default:
                        csp.ImportSubjectPublicKeyInfo(publicKey.FromBase64(), out int bytesRead2);
                        break;
                }
            }
            catch
            {
                throw new ArgumentException("Unable to parse public key.");
            }
        }

        private static void ParsePrivateKey(RSACryptoServiceProvider csp, string privateKey)
        {
            privateKey = privateKey.Trim();
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentException("Invalid null or empty private key.");
            try
            {
                switch (GetKeyEncoding(privateKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonDocument jsonDoc = JsonDocument.Parse(privateKey, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Disallow, MaxDepth = 1 });
                        JsonElement jsonRoot = jsonDoc.RootElement;
                        var parameters = new RSAParameters();
                        parameters.Modulus = jsonRoot.GetProperty(nameof(parameters.Modulus)).GetString()!.ToString().FromBase64();
                        parameters.Exponent = (jsonRoot.GetProperty(nameof(parameters.Exponent)).GetString()!.ToString()).FromBase64();
                        parameters.P = jsonRoot.GetProperty(nameof(parameters.P)).GetString()!.ToString().FromBase64();
                        parameters.Q = jsonRoot.GetProperty(nameof(parameters.Q)).GetString()!.ToString().FromBase64();
                        parameters.DP = jsonRoot.GetProperty(nameof(parameters.DP)).GetString()!.ToString().FromBase64();
                        parameters.DQ = jsonRoot.GetProperty(nameof(parameters.DQ)).GetString()!.ToString().FromBase64();
                        parameters.InverseQ = jsonRoot.GetProperty(nameof(parameters.InverseQ)).GetString()!.ToString().FromBase64();
                        parameters.D = jsonRoot.GetProperty(nameof(parameters.D)).GetString()!.ToString().FromBase64();
                        csp.ImportParameters(parameters);
                        break;
                    case RsaKeyEncoding.Xml:
                        csp.FromXmlString(privateKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        csp.ImportPkcs8PrivateKey((privateKey.Replace(RsaPemPrivateKeyInitialString, string.Empty).Replace(RsaPemPrivateKeyFinalString, string.Empty).Trim()).FromBase64(), out int bytesRead1);
                        break;
                    default:
                        csp.ImportPkcs8PrivateKey(privateKey.FromBase64(), out int bytesRead2);
                        break;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse private key.");
            }
        }

        public static byte[] EncryptRsa(this byte[] data, string publicKey)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePublicKey(csp, publicKey);
                return csp.Encrypt(data, false);
            }
        }

        public static byte[] DecryptRsa(this byte[] data, string privateKey)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePrivateKey(csp, privateKey);
                return csp.Decrypt(data, false);
            }
        }

        public static string EncryptRsa(this string data, string publicKey, IEncoder encoder)
        {
            return encoder.Encode(EncryptRsa(data.ToByteArray(), publicKey));
        }

        public static string DecryptRsa(this string data, string privateKey, IEncoder encoder)
        {
            return DecryptRsa(encoder.Decode(data), privateKey).ToStr();
        }
    }
}



//Para más servicios de encriptación de .NET Framework visitar http://msdn.microsoft.com/en-us/library/92f9ye3s(v=vs.110).aspx
//Para más servicios de encriptación de aplicaciones de la tienda windows visitar http://msdn.microsoft.com/en-us/library/92f9ye3s(v=vs.110).aspx
//Para tamaño correcto de claves http://msdn.microsoft.com/en-us/library/system.security.cryptography.rsacryptoserviceprovider.keysize(v=vs.110).aspx
//Recomendaciones para tamaño de claves revisar:
//http://stackoverflow.com/questions/589834/what-rsa-key-length-should-i-use-for-my-ssl-certificates
//http://pic.dhe.ibm.com/infocenter/zos/v1r13/index.jsp?topic=%2Fcom.ibm.zos.r13.icha700%2Fkeysizec.htm
//http://www.javamex.com/tutorials/cryptography/rsa_key_length.shtml

//public static string SignBase64(string data, HashAlgorithm hashAlgorithm, string privateKey)
//{
//    return HashExtensions.ToBase64(SignRaw(data.ToByteArray(), hashAlgorithm, privateKey));
//}

//public static Boolean VerifyBase64Signature(string data, HashAlgorithm hashAlgorithm, string signature, string publicKey)
//{
//    return VerifyRaw(data.ToByteArray(), hashAlgorithm, HashExtensions.FromBase64(signature), publicKey);
//}

//public static string SignHex(string data, HashAlgorithm hashAlgorithm, string privateKey)
//{
//    return HashExtensions.ToHex(SignRaw(data.ToByteArray(), hashAlgorithm, privateKey));
//}

//public static Boolean VerifyHexSignature(string data, HashAlgorithm hashAlgorithm, string signature, string publicKey)
//{
//    return VerifyRaw(data.ToByteArray(), hashAlgorithm, HashExtensions.FromHex(signature), publicKey);
//}

//public static byte[] SignRaw(byte[] data, HashAlgorithm hashAlgorithm, string privateKey)
//{
//    hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.Md5) ? HashAlgorithm.Sha1 : hashAlgorithm;
//    using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
//    {
//        ParsePrivateKey(csp, privateKey);
//        return csp.SignData(data, hashAlgorithm.ToString());
//    }
//}

//public static Boolean VerifyRaw(byte[] data, HashAlgorithm hashAlgorithm, byte[] signature, string publicKey)
//{
//    hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.Md5) ? HashAlgorithm.Sha1 : hashAlgorithm;
//    using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
//    {
//        ParsePublicKey(csp, publicKey);
//        return csp.VerifyData(data, hashAlgorithm.ToString(), signature);
//    }
//}