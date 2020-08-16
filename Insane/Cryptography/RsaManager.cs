using DevDefined.OAuth.KeyInterop;
using MonoRailsOAuth.Core.KeyInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Insane.Cryptography
{
    public class RsaManager
    {
        public static String SignBase64(String data, HashAlgorithm hashAlgorithm, String privateKey, Boolean keyAsXml, Boolean getUrlSafe = default(Boolean))
        {
            return HashManager.ToBase64(SignRaw(HashManager.ToByteArray(data), hashAlgorithm, privateKey, keyAsXml), false, getUrlSafe);
        }

        public static Boolean VerifyBase64Signature(String data, HashAlgorithm hashAlgorithm, String signature, String publicKey, Boolean keyAsXml)
        {
            return VerifyRaw(HashManager.ToByteArray(data), hashAlgorithm, HashManager.Base64ToByteArray(signature), publicKey, keyAsXml);
        }

        public static String SignHex(String data, HashAlgorithm hashAlgorithm, String privateKey, Boolean keyAsXml, Boolean getUrlSafe = default(Boolean))
        {
            return HashManager.ToHex(SignRaw(HashManager.ToByteArray(data), hashAlgorithm, privateKey, keyAsXml));
        }

        public static Boolean VerifyHexSignature(String data, HashAlgorithm hashAlgorithm, String signature, String publicKey, Boolean keyAsXml = default(Boolean))
        {
            return VerifyRaw(HashManager.ToByteArray(data), hashAlgorithm, HashManager.HexToByteArray(signature), publicKey, keyAsXml);
        }

        public static byte[] SignRaw(byte[] data, HashAlgorithm hashAlgorithm, String privateKey, Boolean keyAsXml = default(Boolean))
        {
            hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.MD5) ? HashAlgorithm.SHA1 : hashAlgorithm;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                if (keyAsXml)
                {
                    csp.FromXmlString(privateKey);
                }
                else
                {
                    csp.ImportParameters(new AsnKeyParser(HashManager.Base64ToByteArray(privateKey)).ParseRSAPrivateKey());
                }
                return csp.SignData(data, hashAlgorithm.ToString());
            }
        }

        public static Boolean VerifyRaw(byte[] data, HashAlgorithm hashAlgorithm, byte[] signature, String publicKey, Boolean keyAsXml = default(Boolean))
        {
            hashAlgorithm = hashAlgorithm.Equals(HashAlgorithm.MD5) ? HashAlgorithm.SHA1 : hashAlgorithm;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                if (keyAsXml)
                {
                    csp.FromXmlString(publicKey);
                }
                else
                {
                    csp.ImportParameters(new AsnKeyParser(HashManager.Base64ToByteArray(publicKey)).ParseRSAPublicKey());
                }
                return csp.VerifyData(data, hashAlgorithm.ToString(), signature);
            }
        }

        public static RsaKeyPair CreateKeyPair(Int32 keySize = 4096, Boolean keyAsXml = default(Boolean), Boolean indentXml = default(Boolean))
        {
            using (RSACryptoServiceProvider Csp = new RSACryptoServiceProvider(keySize))
            {
                if (keyAsXml)
                {
                    return new RsaKeyPair()
                    {
                        PrivateKey = XDocument.Parse(Csp.ToXmlString(true)).ToString(indentXml ? SaveOptions.None : SaveOptions.DisableFormatting),
                        PublicKey = XDocument.Parse(Csp.ToXmlString(false)).ToString(indentXml ? SaveOptions.None : SaveOptions.DisableFormatting)
                    };
                }
                else
                {
                    AsnKeyBuilder.AsnMessage PublicKey = AsnKeyBuilder.PublicKeyToX509(Csp.ExportParameters(false));
                    AsnKeyBuilder.AsnMessage PrivateKey = AsnKeyBuilder.PrivateKeyToPKCS8(Csp.ExportParameters(true));
                    return new RsaKeyPair()
                    {
                        PrivateKey = HashManager.ToBase64(PrivateKey.GetBytes(), false, false),
                        PublicKey = HashManager.ToBase64(PublicKey.GetBytes(), false, false)
                    };
                }
            }
        }

        public static Task<RsaKeyPair> CreateKeyPairAsync(Int32 keySize = 4096, Boolean keyAsXml = default(Boolean), Boolean indentXml = default(Boolean))
        {
            return Task.Run(() =>
            {
                return CreateKeyPair(keySize, keyAsXml, indentXml);
            });
        }

        public static String EncryptToBase64(String data, String publicKey, Boolean keyAsXml = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            var ret = Convert.ToBase64String(EncryptRaw(Encoding.UTF8.GetBytes(data), publicKey, keyAsXml));
            return getUrlSafe ? HashManager.ToSafeUrlBase64(ret) : ret;
        }

        public static Task<String> EncryptToBase64Async(String data, String publicKey, Boolean keyAsXml = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            return Task.Run(()=>
            {
                return EncryptToBase64(data, publicKey, keyAsXml, getUrlSafe);
            });
        }

        public static String EncryptToHex(String plainText, String publicKey, Boolean keyAsXml = default(Boolean))
        {
            return HashManager.ToHex(EncryptRaw(Encoding.UTF8.GetBytes(plainText), publicKey, keyAsXml));
        }

        public static String DecryptFromBase64(String data, String privateKey, Boolean keyAsXml = default(Boolean))
        {
            var ret = DecryptRaw(HashManager.Base64ToByteArray(data), privateKey, keyAsXml);
            return Encoding.UTF8.GetString(ret);
        }

        public static async Task<String> DecryptFromBase64Async(String data, String privateKey, Boolean keyAsXml = default(Boolean))
        {
            return await Task.Run(() =>
            {
                return DecryptFromBase64(data, privateKey, keyAsXml);
            });
        }

        public static String DecryptFromHex(String data, String privateKey, Boolean keyAsXml = default(Boolean))
        {
            var ret = DecryptRaw(HashManager.HexToByteArray(data), privateKey, keyAsXml);
            return Encoding.UTF8.GetString(ret, 0, ret.Length);
        }

        public static byte[] EncryptRaw(byte[] data, String publicKey, Boolean keyAsXml = default(Boolean))
        {
            using (RSACryptoServiceProvider Csp = new RSACryptoServiceProvider())
            {
                if (keyAsXml)
                {

                    Csp.FromXmlString(publicKey);
                    return Csp.Encrypt(data, false);
                }
                else
                {
                    Csp.ImportParameters(new AsnKeyParser(HashManager.Base64ToByteArray(publicKey)).ParseRSAPublicKey());
                    return Csp.Encrypt(data, false);
                }
            }
        }

        public static byte[] DecryptRaw(byte[] data, String privateKey, Boolean keyAsXml = default(Boolean))
        {
            using (RSACryptoServiceProvider Csp = new RSACryptoServiceProvider())
            {
                if (keyAsXml)
                {

                    Csp.FromXmlString(privateKey);
                    return Csp.Decrypt(data, false);
                }
                else
                {
                    Csp.ImportParameters(new AsnKeyParser(HashManager.Base64ToByteArray(privateKey)).ParseRSAPrivateKey());
                    return Csp.Decrypt(data, false);
                }
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