using System;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Cryptography
{
    public class HashManager
    {
        private const int DEFAULT_LINE_BREAKS_LENGTH = 76;

        private static String GetHash(byte[] data, byte[] salt, Boolean isBase64, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean), int iterationNumber = 1)
        {
            byte[] DataWithSaltBytes = new byte[data.Length + salt.Length];
            salt.CopyTo(DataWithSaltBytes, 0);
            data.CopyTo(DataWithSaltBytes, salt.Length);

            byte[] hash = ToRawHash(DataWithSaltBytes, algorithm);
            for (int i = 0; i < iterationNumber; i++)
            {
                hash = ToRawHash(hash, algorithm);
            }

            return isBase64 ? ToBase64(hash, insertLineBreaks, getUrlSafe) : ToHex(hash);
        }

        public static String InsertLineBreaks(String data, int lineBreaksLength = DEFAULT_LINE_BREAKS_LENGTH)
        {
            StringBuilder sb = new StringBuilder();
            int Segments = data.Length / lineBreaksLength;
            if (Segments < 0)
            {
                return data;
            }
            else
            {
                for (int i = 0; i < Segments; i++)
                {
                    sb.AppendLine(data.Substring(i * lineBreaksLength, lineBreaksLength));
                }
                if (Segments * lineBreaksLength < data.Length)
                {
                    sb.AppendLine(data.Substring(Segments * lineBreaksLength));
                }
                String ret = sb.ToString();
                return ret.Substring(0, ret.Length - 2);
            }
        }

        public static HashSaltPair ToBase64Hash(String data, HashAlgorithm algorithm, Boolean insertLineBreaks, Boolean getUrlSafe, int saltSize = 8, int iterationNumber=1)
        {
            HashSaltPair ret = new HashSaltPair();
            byte[] Salt = new byte[saltSize];
            RNGCryptoServiceProvider RandomGenerator = new RNGCryptoServiceProvider();
            RandomGenerator.GetNonZeroBytes(Salt);
            ret.Hash = GetHash(Encoding.UTF8.GetBytes(data), Salt, true, algorithm, insertLineBreaks, getUrlSafe, iterationNumber);
            ret.Salt = ToBase64(Salt, insertLineBreaks, getUrlSafe);
            return ret;
        }

        public static String ToBase64Hash(String data, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            return ToBase64(ToRawHash(data, algorithm), insertLineBreaks, getUrlSafe);
        }

        public static String ToBase64Hash(String data, String salt, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean), int iterationNumber = 1)
        {
            return GetHash(Encoding.UTF8.GetBytes(data), Base64ToByteArray(salt), true, algorithm, insertLineBreaks, getUrlSafe, iterationNumber);
        }

        public static bool MatchesBase64Hash(String data, String hash, String salt, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), int iterationNumber = 1)
        {
            return HashManager.ToUnsafeUrlBase64(hash).Equals(ToBase64Hash(data, salt, algorithm, insertLineBreaks, false, iterationNumber));
        }

        public static String ToBase64(byte[] data, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            var ret = insertLineBreaks ? InsertLineBreaks(Convert.ToBase64String(data)) : Convert.ToBase64String(data);
            return getUrlSafe ? ToSafeUrlBase64(ret) : ret;
        }

        public static String ToBase64(String data, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            var ret = insertLineBreaks ? InsertLineBreaks(Convert.ToBase64String(Encoding.UTF8.GetBytes(data)), DEFAULT_LINE_BREAKS_LENGTH) : Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            return getUrlSafe ? ToSafeUrlBase64(ret) : ret;
        }

        public static byte[] Base64ToByteArray(String base64)
        {
            base64 = ToUnsafeUrlBase64(base64);
            return Convert.FromBase64String(base64);
        }

        public static String Base64ToString(String base64)
        {
            base64 = ToUnsafeUrlBase64(base64);
            var Bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(Bytes, 0, Bytes.Length);
        }

        public static String ToSafeUrlBase64(String base64)
        {
            return base64.Replace("+", "-").Replace("/", "_").Replace("=", ",");
        }

        public static String ToUnsafeUrlBase64(String base64)
        {
            return base64.Replace("-", "+").Replace("_", "/").Replace(",", "=");
        }

        public static HashSaltPair ToHexHash(String data, HashAlgorithm algorithm, int saltSize = 8, int iterationNumber = 1)
        {
            HashSaltPair ret = new HashSaltPair();
            byte[] Salt = new byte[saltSize];
            RNGCryptoServiceProvider RandomGenerator = new RNGCryptoServiceProvider();
            RandomGenerator.GetNonZeroBytes(Salt);
            ret.Hash = GetHash(Encoding.UTF8.GetBytes(data), Salt, false, algorithm, false, false, iterationNumber);
            ret.Salt = ToHex(Salt);
            return ret;
        }

        public static String ToHexHash(String data, HashAlgorithm algorithm)
        {
            return ToHex(ToRawHash(data, algorithm));
        }

        public static String ToHexHash(String data, String Salt, HashAlgorithm algorithm, int iterationNumber = 1)
        {
            return GetHash(Encoding.UTF8.GetBytes(data), HexToByteArray(Salt), false, algorithm, false, false, iterationNumber);
        }

        public static Boolean MatchesHexHash(String data, String saltedHashString, String salt, HashAlgorithm algorithm, int iterationNumber=1)
        {
            return saltedHashString.Equals(ToHexHash(data, salt, algorithm, iterationNumber));
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
            return ToHex(Encoding.UTF8.GetBytes(data));
        }

        public static byte[] HexToByteArray(String hex)
        {
            int Pair = hex.Length % 2;
            byte[] ret = new byte[hex.Length / 2];
            if (Pair == 0)
            {
                for (int i = 0; i < hex.Length / 2; i++)
                {

                    ret[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
            }
            else
            {
                throw new Exception("Cadena con formato incorrecto.");
            }
            return ret;
        }

        public static String HexToString(String hex)
        {
            return Encoding.UTF8.GetString(HexToByteArray(hex), 0, hex.Length);
        }

        public static byte[] ToRawHash(String data, HashAlgorithm algorithm)
        {
            return ToRawHash(ToByteArray(data), algorithm);
        }

        public static byte[] ToRawHash(byte[] data, HashAlgorithm algorithm)
        {
            byte[] hash;
            switch(algorithm)
            {
                case HashAlgorithm.MD5:
                    MD5 md5 = MD5.Create();
                    hash = md5.ComputeHash(data, 0, data.Length);
                    return hash;
                case HashAlgorithm.SHA1:
                    SHA1Managed sha1 = new SHA1Managed();
                    hash = sha1.ComputeHash(data);
                    return hash;
                case HashAlgorithm.SHA256:
                    SHA256Managed sha256 = new SHA256Managed();
                    hash = sha256.ComputeHash(data);
                    return hash; 
                case HashAlgorithm.SHA384:
                    SHA384Managed sha384 = new SHA384Managed();
                    hash = sha384.ComputeHash(data);
                    return hash;
                case HashAlgorithm.SHA512:
                    SHA512Managed sha512 = new SHA512Managed();
                    hash = sha512.ComputeHash(data, 0, data.Length);
                    return hash;
                default:
                    throw new ArgumentException("Invalid Algorithm");
            }
        }

        public static byte[] ToByteArray(String data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static String ToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string ToBase64Hmac(String data, String key, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            switch (algorithm)
            {
                case HashAlgorithm.MD5:
                    using (HMACMD5 hmac = new HMACMD5(keyBytes))
                    {
                        return ToBase64(hmac.ComputeHash(dataBytes), insertLineBreaks, getUrlSafe);
                    }
                case HashAlgorithm.SHA1:
                    using (HMACSHA1 hmac = new HMACSHA1(keyBytes))
                    {
                        return ToBase64(hmac.ComputeHash(dataBytes), insertLineBreaks, getUrlSafe);
                    }
                case HashAlgorithm.SHA256:
                    using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
                    {
                        return ToBase64(hmac.ComputeHash(dataBytes), insertLineBreaks, getUrlSafe);
                    }
                case HashAlgorithm.SHA384:
                    using (HMACSHA384 hmac = new HMACSHA384(keyBytes))
                    {
                        return ToBase64(hmac.ComputeHash(dataBytes), insertLineBreaks, getUrlSafe);
                    }
                case HashAlgorithm.SHA512:
                    using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
                    {
                        return ToBase64(hmac.ComputeHash(dataBytes), insertLineBreaks, getUrlSafe);
                    }
                default:
                    throw new Exception("Invalid Algorithm");
            }
        }

  
        public static string ToHexHmac(String data, String key, HashAlgorithm algorithm, Boolean insertLineBreaks = default(Boolean), Boolean getUrlSafe = default(Boolean))
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            switch (algorithm)
            {
                case HashAlgorithm.MD5:
                    using (HMACMD5 hmac = new HMACMD5(keyBytes))
                    {
                        return ToHex(hmac.ComputeHash(dataBytes));
                    }
                case HashAlgorithm.SHA1:
                    using (HMACSHA1 hmac = new HMACSHA1(keyBytes))
                    {
                        return ToHex(hmac.ComputeHash(dataBytes));
                    }
                case HashAlgorithm.SHA256:
                    using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
                    {
                        return ToHex(hmac.ComputeHash(dataBytes));
                    }
                case HashAlgorithm.SHA384:
                    using (HMACSHA384 hmac = new HMACSHA384(keyBytes))
                    {
                        return ToHex(hmac.ComputeHash(dataBytes));
                    }
                case HashAlgorithm.SHA512:
                    using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
                    {
                        return ToHex(hmac.ComputeHash(dataBytes));
                    }
                default:
                    throw new Exception("Invalid Algorithm.");
            }
        }

    }
}