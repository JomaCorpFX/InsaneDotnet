using CryptSharp.Utility;
using Konscious.Security.Cryptography;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using HashAlgorithm = InsaneIO.Insane.Cryptography.HashAlgorithm;

namespace InsaneIO.Insane.Extensions
{

    [RequiresPreviewFeatures]
    public static class HashExtensions
    {
        public const uint NoLineBreaks = 0;
        public const uint MimeLineBreaksLength = 76;
        public const uint PemLineBreaksLength = 64;

        public const uint Md5HashSizeInBytes = 16;
        public const uint Sha1HashSizeInBytes = 20;
        public const uint Sha256HashSizeInBytes = 32;
        public const uint Sha384HashSizeInBytes = 48;
        public const uint Sha512HashSizeInBytes = 64;

        public const uint HmacKeySize = 16;

        public const uint ScryptIterationsForInteractiveLogin = 2048;
        public const uint ScryptIterationsForEncryption = 1048576;
        public const uint ScryptIterations = 16384;
        public const uint ScryptBlockSize = 8;
        public const uint ScryptParallelism = 1;
        public const uint ScryptDerivedKeyLength = 64;
        public const uint ScryptSaltSize = 16;

        public const uint Argon2DerivedKeyLength = 64;
        public const uint Argon2SaltSize = 16;
        public const uint Argon2Iterations = 2;
        public const uint Argon2MemorySizeInKiB = 16384;
        public const uint Argon2DegreeOfParallelism = 4;


        public static byte[] ToHash(this byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    {
                        using MD5 md5 = MD5.Create();
                        return md5.ComputeHash(data);
                    }
                case HashAlgorithm.Sha1:
                    {
                        using SHA1 sha1 = SHA1.Create();
                        return sha1.ComputeHash(data);
                    }
                case HashAlgorithm.Sha256:
                    {
                        using SHA256 sha256 = SHA256.Create();
                        return sha256.ComputeHash(data);
                    }
                case HashAlgorithm.Sha384:
                    {
                        using SHA384 sha384 = SHA384.Create();
                        return sha384.ComputeHash(data);
                    }
                case HashAlgorithm.Sha512:
                    {
                        using SHA512 sha512 = SHA512.Create();
                        return sha512.ComputeHash(data);
                    }
                default:
                    throw new NotImplementedException(algorithm.ToString());
            }
        }

        public static string ToHash(this string data, IEncoder encoder, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return encoder.Encode(ToHash(data.ToByteArrayUtf8(), algorithm));
        }

        public static byte[] ToHmac(this byte[] data, byte[] key, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    {
                        using HMACMD5 hmac = new(key);
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha1:
                    {
                        using HMACSHA1 hmac = new(key);
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha256:
                    {
                        using HMACSHA256 hmac = new(key);
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha384:
                    {
                        using HMACSHA384 hmac = new(key);
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha512:
                    {
                        using HMACSHA512 hmac = new(key);
                        return hmac.ComputeHash(data);
                    }
                default:
                    throw new NotImplementedException(algorithm.ToString());
            }
        }

        public static string ToHmac(string data, string key, IEncoder encoder, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return encoder.Encode(ToHmac(data.ToByteArrayUtf8(), key.ToByteArrayUtf8(), algorithm));
        }


        public static byte[] ToScrypt(this byte[] data, byte[] salt, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return SCrypt.ComputeDerivedKey(data, salt, (int)iterations, (int)blockSize, (int)parallelism, null, (int)derivedKeyLength);
        }

        public static string ToScrypt(this string data, string salt, IEncoder encoder, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return encoder.Encode(ToScrypt(data.ToByteArrayUtf8(), salt.ToByteArrayUtf8(), iterations, blockSize, parallelism, derivedKeyLength));
        }

        public static byte[] ToArgon2(this byte[] data, byte[] salt, uint iterations = Argon2Iterations, uint memorySizeKiB = Argon2MemorySizeInKiB, uint parallelism = Argon2DegreeOfParallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            switch (variant)
            {
                case Argon2Variant.Argon2d:
                    {
                        using Argon2d argon2d = new(data)
                        {
                            Salt = salt,
                            Iterations = (int)iterations,
                            MemorySize = (int)memorySizeKiB,
                            DegreeOfParallelism = (int)parallelism
                        };
                        return argon2d.GetBytes((int)derivedKeyLength);
                    }
                case Argon2Variant.Argon2i:
                    {
                        using Argon2i argon2i = new(data)
                        {
                            Salt = salt,
                            Iterations = (int)iterations,
                            MemorySize = (int)memorySizeKiB,
                            DegreeOfParallelism = (int)parallelism
                        };
                        return argon2i.GetBytes((int)derivedKeyLength);
                    }
                case Argon2Variant.Argon2id:
                    {
                        using Argon2id argon2id = new(data)
                        {
                            Salt = salt,
                            Iterations = (int)iterations,
                            MemorySize = (int)memorySizeKiB,
                            DegreeOfParallelism = (int)parallelism
                        };
                        return argon2id.GetBytes((int)derivedKeyLength);
                    }
                default:
                    throw new NotImplementedException(variant.ToString());
            }
        }


        public static string ToArgon2(this string data, string salt, IEncoder encoder, uint iterations = Argon2Iterations, uint memorySizeKiB = Argon2MemorySizeInKiB, uint parallelism = Argon2DegreeOfParallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            return encoder.Encode(ToArgon2(data.ToByteArrayUtf8(), salt.ToByteArrayUtf8(), iterations, memorySizeKiB, parallelism, variant, derivedKeyLength));
        }

    }

}