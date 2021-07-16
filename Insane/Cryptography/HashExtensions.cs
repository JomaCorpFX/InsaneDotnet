using CryptSharp.Utility;
using Konscious.Security.Cryptography;
using System;
using System.Linq;
using System.Text;
using Insane.Cryptography;

namespace Insane.Extensions
{
    public static class HashExtensions
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

        public static byte[] ToRawHash(this byte[] data, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    return md5.ComputeHash(data, 0, data.Length);
                case HashAlgorithm.Sha1:
                    System.Security.Cryptography.SHA1Managed sha1 = new ();
                    return sha1.ComputeHash(data);
                case HashAlgorithm.Sha256:
                    System.Security.Cryptography.SHA256Managed sha256 = new ();
                    return sha256.ComputeHash(data);
                case HashAlgorithm.Sha384:
                    System.Security.Cryptography.SHA384Managed sha384 = new ();
                    return sha384.ComputeHash(data);
                case HashAlgorithm.Sha512:
                    System.Security.Cryptography.SHA512Managed sha512 = new ();
                    return sha512.ComputeHash(data, 0, data.Length);
                default:
                    throw new NotImplementedException(algorithm.ToString());
            }
        }

        public static string ToHash(this string data, IEncoder encoder, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return encoder.Encode(ToRawHash(data.ToByteArray(), algorithm));
        }

        public static byte[] ToHmac(this byte[] data, byte[] key, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            switch (algorithm)
            {
                case HashAlgorithm.Md5:
                    using (System.Security.Cryptography.HMACMD5 hmac = new (key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha1:
                    using (System.Security.Cryptography.HMACSHA1 hmac = new (key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha256:
                    using (System.Security.Cryptography.HMACSHA256 hmac = new (key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha384:
                    using (System.Security.Cryptography.HMACSHA384 hmac = new (key))
                    {
                        return hmac.ComputeHash(data);
                    }
                case HashAlgorithm.Sha512:
                    using (System.Security.Cryptography.HMACSHA512 hmac = new (key))
                    {
                        return hmac.ComputeHash(data);
                    }
                default:
                    throw new NotImplementedException(algorithm.ToString());
            }
        }

        public static HmacResult ToHmac(this byte[] data, byte[] key, IEncoder encoder, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return new HmacResult(
                hash: encoder.Encode(ToHmac(data, key, algorithm)),
                key: encoder.Encode(key),
                algorithm: algorithm,
                encoder: encoder
            );
        }

        public static HmacResult ToHmac(this string data, IEncoder encoder, uint keySize = HmacKeySize, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToHmac(data.ToByteArray(), RandomManager.Next((int)keySize), encoder, algorithm);
        }

        public static HmacResult ToHmac(string data, string key, IEncoder encoder, HashAlgorithm algorithm = HashAlgorithm.Sha512)
        {
            return ToHmac(data.ToByteArray(), encoder.Decode(key), encoder, algorithm);
        }


        public static byte[] ToScrypt(this byte[] data, byte[] salt, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return SCrypt.ComputeDerivedKey(data, salt, (int)iterations, (int)blockSize, (int)parallelism, null, (int)derivedKeyLength);
        }

        public static ScryptResult ToScrypt(this byte[] data, byte[] salt, IEncoder encoder, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return new ScryptResult(
                hash: encoder.Encode(ToScrypt(data, salt, iterations, blockSize, parallelism, derivedKeyLength)),
                salt: encoder.Encode(salt),
                iterations: iterations,
                blockSize: blockSize,
                parallelism: parallelism,
                derivedKeyLength: derivedKeyLength,
                encoder
            );
        }

        public static ScryptResult ToScrypt(this string data, string salt, IEncoder encoder, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            return ToScrypt(data.ToByteArray(), encoder.Decode(salt), encoder, iterations, blockSize, parallelism, derivedKeyLength);
        }

        public static ScryptResult ToScrypt(this string data, IEncoder encoder, uint saltSize = ScryptSaltSize, uint iterations = ScryptIterations, uint blockSize = ScryptBlockSize, uint parallelism = ScryptParallelism, uint derivedKeyLength = ScryptDerivedKeyLength)
        {
            byte[] salt = RandomManager.Next((int)saltSize);
            return ToScrypt(data.ToByteArray(), salt, encoder, iterations, blockSize, parallelism, derivedKeyLength);
        }

        public static byte[] ToArgon2(this byte[] data, byte[] salt, uint iterations, uint memorySizeKiB, uint parallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
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
                    throw new NotImplementedException(variant.ToString());
            }
        }


        public static Argon2Result ToArgon2(this byte[] data, byte[] salt, IEncoder encoder, uint iterations, uint memorySizeKiB, uint parallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            return new Argon2Result
            (
                hash: encoder.Encode(ToArgon2(data, salt, iterations, memorySizeKiB, parallelism, variant, derivedKeyLength)),
                salt: encoder.Encode(salt),
                variant: variant,
                iterations: iterations,
                memorySizeKiB: memorySizeKiB,
                parallelism: parallelism,
                derivedKeyLength: derivedKeyLength,
                encoder: encoder
            );
        }

        public static Argon2Result ToArgon2(this string data, string salt, IEncoder encoder, uint iterations, uint memorySizeKiB, uint parallelism, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            return ToArgon2(data.ToByteArray(), salt.ToByteArray(), encoder, iterations, memorySizeKiB, parallelism, variant, derivedKeyLength);
        }

        public static Argon2Result ToArgon2(this string data, IEncoder encoder, uint iterations, uint memorySizeKiB, uint parallelism, uint saltSize = Argon2SaltSize, Argon2Variant variant = Argon2Variant.Argon2id, uint derivedKeyLength = Argon2DerivedKeyLength)
        {
            byte[] salt = RandomManager.Next((int)saltSize);
            return ToArgon2(data.ToByteArray(), salt, encoder, iterations, memorySizeKiB, parallelism, variant, derivedKeyLength);
        }
    }

}