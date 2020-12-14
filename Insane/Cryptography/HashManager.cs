using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Cryptography
{
    public class HashManager
    {
        public const int NoLineBreaks = 0;
        public const int MimeLineBreaksLength = 76;
        public const int PemLineBreaksLength = 64;

        public static byte[] ToByteArray(String data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string InsertLineBreaks(String data, uint lineBreaksLength = MimeLineBreaksLength)
        {
            int distance = (int)lineBreaksLength;
            if(lineBreaksLength==0)
            {
                return data;
            }
            StringBuilder sb = new StringBuilder();
            int Segments = data.Length / distance;
            if (Segments < 0)
            {
                return data;
            }
            else
            {
                for (int i = 0; i < Segments; i++)
                {
                    sb.AppendLine(data.Substring(i * distance, distance));
                }
                if (Segments * lineBreaksLength < data.Length)
                {
                    sb.AppendLine(data.Substring(Segments * distance));
                }
                return sb.ToString().Trim();
            }
        }

        

        public static string ToBase64(byte[] data, uint lineBreaksLength = NoLineBreaks, bool removePadding= false)
        {
            return InsertLineBreaks(Convert.ToBase64String(data), lineBreaksLength).Replace("=", removePadding? "": "=");
        }

        public static string ToBase64(string data, uint lineBreaksLength = NoLineBreaks, bool removePadding = false)
        {
            return ToBase64(ToByteArray(data), lineBreaksLength, removePadding);
        }

        public static String ToHex(byte[] data)
        {
            StringBuilder ret = new StringBuilder(String.Empty);
            foreach (byte Value in data)
            {
                ret.Append(Value.ToString("x2"));
            }
            return ret.ToString();
        }

        public static String ToHex(String data)
        {
            return ToHex(ToByteArray(data));
        }

        public static string ToUrlSafeBase64(byte[] data)
        {
            return ToBase64(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        public static string ToFilenameSafeBase64(byte[] data)
        {
            return ToUrlSafeBase64(data);
        }

        public static string ToUrlEncodedBase64(byte[] data)
        {
            return ToBase64(data).Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D");
        }

        public static byte[] FromBase64(string data)
        {
            data = data.Replace("%2B", "+").Replace("%2F", "/").Replace("%3D", "=")
                .Replace("-", "+").Replace("_", "/").Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\r\n", string.Empty);
            if (!data.EndsWith("="))
            {
                int modulo = data.Length % 4;
                data = modulo == 0 ? data : data.PadRight(data.Length + modulo, '=');
            }
            return Convert.FromBase64String(data);
        }

        public static byte[] FromHex(string data)
        {
            int Pair = data.Length % 2;
            byte[] ret = new byte[data.Length / 2];
            if (Pair == 0)
            {
                for (int i = 0; i < data.Length / 2; i++)
                {

                    ret[i] = Convert.ToByte(data.Substring(i * 2, 2), 16);
                }
            }
            else
            {
                throw new Exception("Invalid hex string.");
            }
            return ret;
        }

        public static byte[] ToRawHash(byte[] data, HashAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    MD5 md5 = MD5.Create();
                    return md5.ComputeHash(data, 0, data.Length);
                case HashAlgorithm.Sha1:
                    SHA1Managed sha1 = new SHA1Managed();
                    return sha1.ComputeHash(data);
                case HashAlgorithm.Sha256:
                    SHA256Managed sha256 = new SHA256Managed();
                    return  sha256.ComputeHash(data);
                case HashAlgorithm.Sha384:
                    SHA384Managed sha384 = new SHA384Managed();
                    return sha384.ComputeHash(data);
                case HashAlgorithm.Sha512:
                    SHA512Managed sha512 = new SHA512Managed();
                    return sha512.ComputeHash(data, 0, data.Length);
                default:
                    throw new ArgumentException("Invalid hash algorithm");
            }
        }

        public static string ToBase64Hash(string data, HashAlgorithm algorithm, uint linebreaksLength = NoLineBreaks)
        {
            return ToBase64(ToRawHash(ToByteArray(data), algorithm), linebreaksLength);
        }

        public static string ToHexHash(string data, HashAlgorithm algorithm)
        {
            return ToHex(ToRawHash(ToByteArray(data), algorithm));
        }

        public static string ToBase64Hash(byte[] data, HashAlgorithm algorithm, uint linebreaksLength = NoLineBreaks)
        {
            return ToBase64(ToRawHash(data, algorithm), linebreaksLength);
        }

        public static string ToHexHash(byte[] data, HashAlgorithm algorithm)
        {
            return ToHex(ToRawHash(data, algorithm));
        }

        public static byte[] ToRawHmac(byte[] data, byte[] key, HashAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    using (HMACMD5 hmac = new HMACMD5(key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha1:
                    using (HMACSHA1 hmac = new HMACSHA1(key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha256:
                    using (HMACSHA256 hmac = new HMACSHA256(key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha384:
                    using (HMACSHA384 hmac = new HMACSHA384(key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha512:
                    using (HMACSHA512 hmac = new HMACSHA512(key))
                    {
                        return hmac.ComputeHash(data);
                    }
                default:
                    throw new Exception("Invalid hash Algorithm");
            }
        }

        public static string ToBase64Hmac(byte[] data, byte[] key, HashAlgorithm algorithm, uint linebreaksLength = NoLineBreaks)
        {
            return ToBase64(ToRawHmac(data, key, algorithm), linebreaksLength);
        }

        public static string ToHexHmac(byte[] data, byte[] key, HashAlgorithm algorithm)
        {
            return ToHex(ToRawHmac(data, key, algorithm));
        }

        public static string ToBase64Hmac(string data, string key, HashAlgorithm algorithm, uint linebreaksLength = NoLineBreaks)
        {
            return ToBase64( ToRawHmac(ToByteArray(data), ToByteArray(key), algorithm), linebreaksLength);
        }

        public static string ToHexHmac(string data, string key, HashAlgorithm algorithm)
        {
            return ToHex(ToRawHmac(ToByteArray(data), ToByteArray(key), algorithm));
        }

        public static string Base64ToUrlSafeBase64(string base64)
        {
            try
            {
                return ToUrlSafeBase64(FromBase64(base64));
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid base64 string.");
            }
        }

        public static string Base64ToFilenameSafeBase64(string base64)
        {
            return Base64ToUrlSafeBase64(base64);
        }

        public static string Base64ToUrlEncodedBase64(string base64)
        {
            try
            {
                return ToUrlEncodedBase64(FromBase64(base64));
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid base64 string.");
            }
        }

    }
}