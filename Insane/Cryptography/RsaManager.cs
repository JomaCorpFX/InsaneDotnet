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

namespace Insane.Cryptography
{
    public class RsaManager
    {

        private const string RsaPemPrivateKeyInitialString = "-----BEGIN PRIVATE KEY-----";
        private const string RsaPemPrivateKeyFinalString = "-----END PRIVATE KEY-----";
        private const string RsaPemPublicKeyInitialString = "-----BEGIN PUBLIC KEY-----";
        private const string RsaPemPublicKeyFinalString = "-----END PUBLIC KEY-----";

        private const string PemPublicAndPrivateKeyPattern = "(-----BEGIN PUBLIC KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PRIVATE KEY-----)"; //https://regex101.com/r/mGnr7I/1  (-----BEGIN PUBLIC KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PRIVATE KEY-----)
        private const string JsonPublicAndPrivateKeyPattern = "(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"P\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Q\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DP\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"InverseQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"D\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\\s*\\}\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(12)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(11)|,)()){2}\\s*\\}\\s*\\11\\12)"; //https://regex101.com/r/FNRFqV/2  (\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"P"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Q"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DP"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"InverseQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"D"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\s*\}\s*\2\3\4\5\6\7\8\9)|(\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(12)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(11)|,)()){2}\s*\}\s*\11\12)
        private const string XmlPublicAndPrivateKeyPattern = "(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()|\\s*<\\s*P\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*P\\s*>()|\\s*<\\s*Q\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Q\\s*>()|\\s*<\\s*DP\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DP\\s*>()|\\s*<\\s*DQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DQ\\s*>()|\\s*<\\s*InverseQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*InverseQ\\s*>()|\\s*<\\s*D\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*D\\s*>()){8}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()){2}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\11\\12)"; //https://regex101.com/r/fQV2VN/4  (\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()|\s*<\s*P\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*P\s*>()|\s*<\s*Q\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Q\s*>()|\s*<\s*DP\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DP\s*>()|\s*<\s*DQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DQ\s*>()|\s*<\s*InverseQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*InverseQ\s*>()|\s*<\s*D\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*D\s*>()){8}\s*<\/\s*RSAKeyValue\s*>\s*\2\3\4\5\6\7\8\9)|(\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()){2}\s*<\/\s*RSAKeyValue\s*>\s*\11\12)
        public static string SignBase64(string data, HashAlgorithm hashAlgorithm, string privateKey)
        {
            return HashManager.ToBase64(SignRaw(HashManager.ToByteArray(data), hashAlgorithm, privateKey));
        }

        public static Boolean VerifyBase64Signature(string data, HashAlgorithm hashAlgorithm, string signature, string publicKey)
        {
            return VerifyRaw(HashManager.ToByteArray(data), hashAlgorithm, HashManager.FromBase64(signature), publicKey);
        }

        public static string SignHex(string data, HashAlgorithm hashAlgorithm, string privateKey)
        {
            return HashManager.ToHex(SignRaw(HashManager.ToByteArray(data), hashAlgorithm, privateKey));
        }

        public static Boolean VerifyHexSignature(string data, HashAlgorithm hashAlgorithm, string signature, string publicKey)
        {
            return VerifyRaw(HashManager.ToByteArray(data), hashAlgorithm, HashManager.FromHex(signature), publicKey);
        }

        public static byte[] SignRaw(byte[] data, HashAlgorithm hashAlgorithm, string privateKey)
        {
            hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.Md5) ? HashAlgorithm.Sha1 : hashAlgorithm;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePrivateKey(csp, privateKey);
                return csp.SignData(data, hashAlgorithm.ToString());
            }
        }

        public static Boolean VerifyRaw(byte[] data, HashAlgorithm hashAlgorithm, byte[] signature, string publicKey)
        {
            hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.Md5) ? HashAlgorithm.Sha1 : hashAlgorithm;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePublicKey(csp, publicKey);
                return csp.VerifyData(data, hashAlgorithm.ToString(), signature);
            }
        }

        public static RsaKeyPair CreateKeyPair(uint keySize = 4096, RsaKeyEncoding encoding = RsaKeyEncoding.Ber, bool indent = true)
        {
            using (RSACryptoServiceProvider Csp = new RSACryptoServiceProvider((int)keySize))
            {
                switch (encoding)
                {
                    case RsaKeyEncoding.Xml:
                        return new RsaKeyPair(
                        publicKey: XDocument.Parse(Csp.ToXmlString(false)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting),
                        privateKey: XDocument.Parse(Csp.ToXmlString(true)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting)
                    );
                    case RsaKeyEncoding.Json:
                        RSAParameters parameters = Csp.ExportParameters(true);
                        var pubKey = new
                        {
                            Modulus = HashManager.ToBase64(parameters.Modulus!),
                            Exponent = HashManager.ToBase64(parameters.Exponent!),
                        };
                        var privKey = new
                        {
                            Modulus = HashManager.ToBase64(parameters.Modulus!),
                            Exponent = HashManager.ToBase64(parameters.Exponent!),
                            P = HashManager.ToBase64(parameters.P!),
                            Q = HashManager.ToBase64(parameters.Q!),
                            DP = HashManager.ToBase64(parameters.DP!),
                            DQ = HashManager.ToBase64(parameters.DQ!),
                            InverseQ = HashManager.ToBase64(parameters.InverseQ!),
                            D = HashManager.ToBase64(parameters.D!)
                        };
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = indent
                        };
                        return new RsaKeyPair(JsonSerializer.Serialize(pubKey, options), JsonSerializer.Serialize(privKey, options));
                    case RsaKeyEncoding.Pem:
                        return new RsaKeyPair($"{RsaPemPublicKeyInitialString}\n{HashManager.ToBase64(Csp.ExportSubjectPublicKeyInfo(), HashManager.PemLineBreaksLength)}\n{RsaPemPublicKeyFinalString}",
                                              $"{RsaPemPrivateKeyInitialString}\n{HashManager.ToBase64(Csp.ExportPkcs8PrivateKey(), HashManager.PemLineBreaksLength)}\n{RsaPemPrivateKeyFinalString}");
                    default:
                        return new RsaKeyPair(publicKey: HashManager.ToBase64(Csp.ExportSubjectPublicKeyInfo()), privateKey: HashManager.ToBase64(Csp.ExportPkcs8PrivateKey()));

                }


            }
        }

        public static Task<RsaKeyPair> CreateKeyPairAsync(uint keySize = 4096, RsaKeyEncoding encoding = RsaKeyEncoding.Ber, bool indent = true)
        {
            return Task.Run(() =>
            {
                return CreateKeyPair(keySize, encoding, indent);
            });
        }

        public static string EncryptToBase64(string data, string publicKey)
        {
            return HashManager.ToBase64(EncryptRaw(HashManager.ToByteArray(data), publicKey));
        }

        public static string EncryptToHex(string plainText, string publicKey)
        {
            return HashManager.ToHex(EncryptRaw(HashManager.ToByteArray(plainText), publicKey));
        }

        public static string DecryptFromBase64(string data, string privateKey)
        {
            return HashManager.ToString(DecryptRaw(HashManager.FromBase64(data), privateKey));
        }

        public static async Task<String> DecryptFromBase64Async(string data, string privateKey)
        {
            return await Task.Run(() =>
            {
                return DecryptFromBase64(data, privateKey);
            });
        }

        public static string DecryptFromHex(string data, string privateKey)
        {
            return HashManager.ToString(DecryptRaw(HashManager.FromHex(data), privateKey));
        }

        private static RsaKeyEncoding GetKeyEncoding(string key)
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
            try
            {
                switch (GetKeyEncoding(publicKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonDocument jsonDoc = JsonDocument.Parse(publicKey, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Disallow, MaxDepth = 1 });
                        JsonElement jsonRoot = jsonDoc.RootElement;
                        var parameters = new RSAParameters();
                        parameters.Modulus = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.Modulus)).GetString()!.ToString());
                        parameters.Exponent = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.Exponent)).GetString()!.ToString());
                        csp.ImportParameters(parameters);
                        break;
                    case RsaKeyEncoding.Xml:
                        csp.FromXmlString(publicKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        csp.ImportSubjectPublicKeyInfo(HashManager.FromBase64(publicKey.Replace(RsaPemPublicKeyInitialString, string.Empty).Replace(RsaPemPublicKeyFinalString, string.Empty).Trim()), out int bytesRead1);
                        break;
                    default:

                        csp.ImportSubjectPublicKeyInfo(HashManager.FromBase64(publicKey), out int bytesRead2);
                        break;
                }
            }
            catch
            {
                throw new Exception("Unable to parse public key.");
            }
        }

        private static void ParsePrivateKey(RSACryptoServiceProvider csp, string privateKey)
        {
            try
            {
                switch (GetKeyEncoding(privateKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonDocument jsonDoc = JsonDocument.Parse(privateKey, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Disallow, MaxDepth = 1 });
                        JsonElement jsonRoot = jsonDoc.RootElement;
                        var parameters = new RSAParameters();
                        parameters.Modulus = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.Modulus)).GetString()!.ToString());
                        parameters.Exponent = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.Exponent)).GetString()!.ToString());
                        parameters.P = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.P)).GetString()!.ToString());
                        parameters.Q = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.Q)).GetString()!.ToString());
                        parameters.DP = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.DP)).GetString()!.ToString());
                        parameters.DQ = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.DQ)).GetString()!.ToString());
                        parameters.InverseQ = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.InverseQ)).GetString()!.ToString());
                        parameters.D = HashManager.FromBase64(jsonRoot.GetProperty(nameof(parameters.D)).GetString()!.ToString());
                        csp.ImportParameters(parameters);
                        break;
                    case RsaKeyEncoding.Xml:
                        csp.FromXmlString(privateKey);
                        break;
                    case RsaKeyEncoding.Pem:
                        csp.ImportPkcs8PrivateKey(HashManager.FromBase64(privateKey.Replace(RsaPemPrivateKeyInitialString, string.Empty).Replace(RsaPemPrivateKeyFinalString, string.Empty).Trim()), out int bytesRead1);
                        break;
                    default:
                        csp.ImportPkcs8PrivateKey(HashManager.FromBase64(privateKey), out int bytesRead2);
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to parse private key.");
            }
        }

        public static byte[] EncryptRaw(byte[] data, string publicKey)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePublicKey(csp, publicKey);
                return csp.Encrypt(data, false);
            }
        }

        public static byte[] DecryptRaw(byte[] data, string privateKey)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                ParsePrivateKey(csp, privateKey);
                return csp.Decrypt(data, false);
            }
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