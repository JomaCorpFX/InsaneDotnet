using CryptSharp.Utility;
using Konscious.Security.Cryptography;
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

        public const uint ScryptIterations = 32768;
        public const uint ScryptBlockSize = 8;
        public const uint ScryptParallelism = 1;
        public const uint ScryptDerivedKeyLength = 64;
        public const uint ScryptSaltSize = 16;

        public const uint Argon2DerivedKeyLength = 64;
        public const uint Argon2SaltSize = 16;

        public const uint HmacKeySize = 16;

        public static byte[] ToByteArray(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string InsertLineBreaks(string data, uint lineBreaksLength = MimeLineBreaksLength)
        {
            int distance = (int)lineBreaksLength;
            if (lineBreaksLength == 0)
            {
                return data;
            }
            StringBuilder sb = new StringBuilder();
            int Segments = data.Length / distance;
            if (Segments < 0)
            {
                return data.Trim();
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

        public static string ToBase64(byte[] data, uint lineBreaksLength = NoLineBreaks, bool removePadding = false)
        {
            return InsertLineBreaks(Convert.ToBase64String(data), lineBreaksLength).Replace("=", removePadding ? "" : "=");
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

        public static String ToHex(string data)
        {
            return ToHex(ToByteArray(data));
        }

        public static byte[] FromBase64(string data)
        {
            int modulo = data.Length % 4;
            data = data.Replace("%2B", "+").Replace("%2F", "/").Replace("%3D", "=")
                .Replace("-", "+").Replace("_", "/").Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\r\n", string.Empty)
                .PadRight(data.Length + (modulo>0 ? 4 - modulo: 0), '=');
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
                throw new SystemException("Invalid hex string.");
            }
            return ret;
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

        public static byte[] ToRawHash(byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha512)
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
                    return sha256.ComputeHash(data);
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

        public static string ToBase64Hash(byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToBase64(ToRawHash(data, algorithm));
        }

        public static string ToBase64Hash(string data, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToBase64Hash(ToByteArray(data), algorithm);
        }

        public static byte[] ToRawHmac(byte[] data, byte[] key, HashAlgorithm algorithm = HashAlgorithm.Sha512)
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

        public static HmacResult ToBase64Hmac(byte[] data, byte[] key, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return new HmacResult
            (
                hash: ToBase64(ToRawHmac(data, key, algorithm)),
                key: ToBase64(key),
                algorithm: algorithm
            );
        }

        public static HmacResult ToBase64Hmac(byte[] data, uint keySize = HmacKeySize, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToBase64Hmac(data, RandomManager.Next((int)keySize), algorithm);
        }

        public static HmacResult ToBase64Hmac(string data, string key, bool isBase64Key = true, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToBase64Hmac(ToByteArray(data), isBase64Key ? FromBase64(key) : ToByteArray(key), algorithm);
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

        public static byte[] ToRawScrypt(byte[] data, byte[] salt, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return SCrypt.ComputeDerivedKey(data, salt, (int)iterations, (int)blockSize, (int)parallelism, null, (int)derivedKeyLength);
        }


        public static ScryptResult ToBase64Scrypt(string data, string salt, bool isBase64Salt, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return new ScryptResult(           
                ToBase64(ToRawScrypt(ToByteArray( data), isBase64Salt? FromBase64(salt):ToByteArray( salt), iterations, blockSize, parallelism, derivedKeyLength)),
                isBase64Salt ? salt: ToBase64(salt),
                iterations,
                blockSize,
                parallelism,
                derivedKeyLength
            );
        }

        public static ScryptResult ToBase64Scrypt(string data, uint saltSize = ScryptSaltSize, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            byte[] salt = RandomManager.Next((int)saltSize);
            return ToBase64Scrypt(data, ToBase64(salt), true, iterations, blockSize, parallelism, derivedKeyLength);
        }


        public static byte[] ToRawArgon2(byte[] data, byte[] salt, uint iterations, uint memorySizeKiB, uint parallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {

            switch (variant)
            {
                case Argon2Variant.Argon2d:
                    Argon2d argon2d = new Argon2d(data);
                    argon2d.Salt = salt;
                    argon2d.Iterations = (int)iterations;
                    argon2d.MemorySize = (int)memorySizeKiB;
                    argon2d.DegreeOfParallelism = (int)parallelism;
                    return argon2d.GetBytes((int)derivedKeyLength);
                case Argon2Variant.Argon2i:
                    Argon2i argon2i = new Argon2i(data);
                    argon2i.Salt = salt;
                    argon2i.Iterations = (int)iterations;
                    argon2i.MemorySize = (int)memorySizeKiB;
                    argon2i.DegreeOfParallelism = (int)parallelism;
                    return argon2i.GetBytes((int)derivedKeyLength);
                case Argon2Variant.Argon2id:
                    Argon2id argon2id = new Argon2id(data);
                    argon2id.Salt = salt;
                    argon2id.Iterations = (int)iterations;
                    argon2id.MemorySize = (int)memorySizeKiB;
                    argon2id.DegreeOfParallelism = (int)parallelism;
                    return argon2id.GetBytes((int)derivedKeyLength);
                default:
                    throw new Exception("Invalid Argon2 variant");
            }
        }


        public static Argon2Result ToBase64Argon2(string data, string salt, bool isBase64Salt, uint iterations, uint memorySizeKiB, uint parallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            return new Argon2Result
            (
                hash: ToBase64(ToRawArgon2(ToByteArray(data), isBase64Salt ? FromBase64(salt) : ToByteArray(salt), iterations, memorySizeKiB, parallelism, variant, derivedKeyLength)),
                salt: isBase64Salt ? salt : ToBase64(salt),
                variant: variant,
                iterations: iterations,
                memorySizeKiB: memorySizeKiB,
                parallelism: parallelism,
                derivedKeyLength: derivedKeyLength
            );
        }

        public static Argon2Result ToBase64Argon2(string data, uint iterations, uint memorySizeKiB, uint parallelism, uint saltSize = Argon2SaltSize, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            byte[] salt = RandomManager.Next((int)saltSize);
            return ToBase64Argon2(data, ToBase64(salt), true, iterations, memorySizeKiB, parallelism, variant, derivedKeyLength);
        }

    }

}