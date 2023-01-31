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
        private const string PemPublicAndPrivateKeyPattern = "(-----BEGIN PUBLIC KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\\n|\\r|\\r\\n)([0-9a-zA-Z\\+\\/=]{64}(\\n|\\r|\\r\\n))*([0-9a-zA-Z\\+\\/=]{1,63}(\\n|\\r|\\r\\n))?-----END PRIVATE KEY-----)"; //https://regex101.com/r/mGnr7I/1  (-----BEGIN PUBLIC KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PUBLIC KEY-----)|(-----BEGIN PRIVATE KEY-----(\n|\r|\r\n)([0-9a-zA-Z\+\/=]{64}(\n|\r|\r\n))*([0-9a-zA-Z\+\/=]{1,63}(\n|\r|\r\n))?-----END PRIVATE KEY-----)
        private const string JsonPublicAndPrivateKeyPattern = "(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"P\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"Q\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DP\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"DQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"InverseQ\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\\s*\"D\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\\s*\\}\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*\\{(?:\\s*\"Modulus\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(12)|,)()|\\s*\"Exponent\"\\s*:\\s*\"[a-zA-Z\\d\\+\\/\\\\]+={0,2}\"\\s*(?(11)|,)()){2}\\s*\\}\\s*\\11\\12)"; //https://regex101.com/r/FNRFqV/2  (\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(4)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"P"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(5)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"Q"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(6)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DP"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(7)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"DQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(8)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"InverseQ"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(9)|,)|,)|,)|,)|,)|,)|,)()|\s*"D"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(2)(?(3)(?(4)(?(5)(?(6)(?(7)(?(8)|,)|,)|,)|,)|,)|,)|,)()){8}\s*\}\s*\2\3\4\5\6\7\8\9)|(\s*\{(?:\s*"Modulus"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(12)|,)()|\s*"Exponent"\s*:\s*"[a-zA-Z\d\+\/\\]+={0,2}"\s*(?(11)|,)()){2}\s*\}\s*\11\12)
        private const string XmlPublicAndPrivateKeyPattern = "(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()|\\s*<\\s*P\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*P\\s*>()|\\s*<\\s*Q\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Q\\s*>()|\\s*<\\s*DP\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DP\\s*>()|\\s*<\\s*DQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*DQ\\s*>()|\\s*<\\s*InverseQ\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*InverseQ\\s*>()|\\s*<\\s*D\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*D\\s*>()){8}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\2\\3\\4\\5\\6\\7\\8\\9)|(\\s*<\\s*RSAKeyValue\\s*>\\s*(?:\\s*<\\s*Modulus\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Modulus\\s*>()|\\s*<\\s*Exponent\\s*>\\s*[a-zA-Z0-9\\+\\/]+={0,2}\\s*<\\/\\s*Exponent\\s*>()){2}\\s*<\\/\\s*RSAKeyValue\\s*>\\s*\\11\\12)"; //https://regex101.com/r/fQV2VN/4  (\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()|\s*<\s*P\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*P\s*>()|\s*<\s*Q\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Q\s*>()|\s*<\s*DP\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DP\s*>()|\s*<\s*DQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*DQ\s*>()|\s*<\s*InverseQ\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*InverseQ\s*>()|\s*<\s*D\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*D\s*>()){8}\s*<\/\s*RSAKeyValue\s*>\s*\2\3\4\5\6\7\8\9)|(\s*<\s*RSAKeyValue\s*>\s*(?:\s*<\s*Modulus\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Modulus\s*>()|\s*<\s*Exponent\s*>\s*[a-zA-Z0-9\+\/]+={0,2}\s*<\/\s*Exponent\s*>()){2}\s*<\/\s*RSAKeyValue\s*>\s*\11\12)


        public static RsaKeyPair CreateRsaKeyPair(this uint keySize, RsaKeyEncoding encoding = RsaKeyEncoding.Ber, bool indent = true)
        {
            RsaKeyPair result;
            using RSA Csp = RSA.Create((int)keySize);
            switch (encoding)
            {
                case RsaKeyEncoding.Xml:
                    result = new RsaKeyPair
                    {
                        PublicKey = XDocument.Parse(Csp.ToXmlString(false)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting),
                        PrivateKey = XDocument.Parse(Csp.ToXmlString(true)).ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting)
                    };
                    break;
                case RsaKeyEncoding.Json:
                    RSAParameters parameters = Csp.ExportParameters(true);
                    var pubKey = new
                    {
                        Modulus = parameters.Modulus!.ToBase64(),
                        Exponent = parameters.Exponent!.ToBase64(),
                    };
                    var privKey = new
                    {
                        Modulus = parameters.Modulus!.ToBase64(),
                        Exponent = parameters.Exponent!.ToBase64(),
                        P = parameters.P!.ToBase64(),
                        Q = parameters.Q!.ToBase64(),
                        DP = parameters.DP!.ToBase64(),
                        DQ = parameters.DQ!.ToBase64(),
                        InverseQ = parameters.InverseQ!.ToBase64(),
                        D = parameters.D!.ToBase64()
                    };
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = indent
                    };
                    result = new RsaKeyPair
                    {
                        PublicKey = JsonSerializer.Serialize(pubKey, options),
                        PrivateKey = JsonSerializer.Serialize(privKey, options)
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
            ValidateRsaPublicKey(result.PublicKey);
            ValidateRsaPrivateKey(result.PrivateKey);
            return result;
        }

        public static void ValidateRsaPublicKey(this string publicKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty public key.");
            using RSA csp = RSA.Create();
            ParsePublicKey(csp, publicKey);
        }

        public static void ValidateRsaPrivateKey(this string privateKey)
        {
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentException("Invalid null or empty private key.");
            using RSA csp = RSA.Create();
            ParsePrivateKey(csp, privateKey);
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

        private static void ParsePublicKey(RSA rsa, string publicKey)
        {
            publicKey = publicKey.Trim();
            if (string.IsNullOrWhiteSpace(publicKey)) throw new ArgumentException("Invalid null or empty private key.");
            try
            {
                switch (GetKeyEncoding(publicKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonNode jsonNode = JsonNode.Parse(publicKey)!;
                        var parameters = new RSAParameters
                        {
                            Modulus = jsonNode[nameof(RSAParameters.Modulus)]!.GetValue<string>().FromBase64(),
                            Exponent = jsonNode[nameof(RSAParameters.Exponent)]!.GetValue<string>().FromBase64()
                        };
                        rsa.ImportParameters(parameters);
                        break;
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
                switch (GetKeyEncoding(privateKey))
                {
                    case RsaKeyEncoding.Json:
                        JsonNode jsonNode = JsonNode.Parse(privateKey)!;
                        var parameters = new RSAParameters
                        {
                            Modulus = jsonNode[nameof(RSAParameters.Modulus)]!.GetValue<string>().FromBase64(),
                            Exponent = jsonNode[nameof(RSAParameters.Exponent)]!.GetValue<string>().FromBase64(),
                            P = jsonNode[nameof(RSAParameters.P)]!.GetValue<string>().FromBase64(),
                            Q = jsonNode[nameof(RSAParameters.Q)]!.GetValue<string>().FromBase64(),
                            DP = jsonNode[nameof(RSAParameters.DP)]!.GetValue<string>().FromBase64(),
                            DQ = jsonNode[nameof(RSAParameters.DQ)]!.GetValue<string>().FromBase64(),
                            InverseQ = jsonNode[nameof(RSAParameters.InverseQ)]!.GetValue<string>().FromBase64(),
                            D = jsonNode[nameof(RSAParameters.D)]!.GetValue<string>().FromBase64()
                        };
                        rsa.ImportParameters(parameters);
                        break;
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

        public static byte[] EncryptRsa(this byte[] data, string publicKey, RsaPadding padding = RsaPadding.Oaep256)
        {
            using RSA rsa = RSA.Create();
            ParsePublicKey(rsa, publicKey);
            RSAEncryptionPadding rsaPadding = padding switch
            {
                RsaPadding.Pkcs1 => RSAEncryptionPadding.Pkcs1,
                RsaPadding.Oaep256 => RSAEncryptionPadding.OaepSHA256,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            return rsa.Encrypt(data, rsaPadding);
        }

        public static string EncryptRsa(this string data, string publicKey, IEncoder encoder, RsaPadding padding = RsaPadding.Oaep256)
        {
            return encoder.Encode(EncryptRsa(data.ToByteArrayUtf8(), publicKey, padding));
        }

        public static byte[] DecryptRsa(this byte[] data, string privateKey, RsaPadding padding = RsaPadding.Oaep256)
        {
            using RSA rsa = RSA.Create();
            ParsePrivateKey(rsa, privateKey);
            RSAEncryptionPadding rsaPadding = padding switch
            {
                RsaPadding.Pkcs1 => RSAEncryptionPadding.Pkcs1,
                RsaPadding.Oaep256 => RSAEncryptionPadding.OaepSHA256,
                _ => throw new NotImplementedException(padding.ToString()),
            };
            return rsa.Decrypt(data, rsaPadding);
        }

        public static string DecryptRsa(this string data, string privateKey, IEncoder encoder, RsaPadding padding = RsaPadding.Oaep256)
        {
            return DecryptRsa(encoder.Decode(data), privateKey, padding).ToStringUtf8();
        }
    }
}