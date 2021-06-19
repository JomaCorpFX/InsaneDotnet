using Insane.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class Argon2Result
    {
        public string Hash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public Argon2Variant Variant { get; set; }
        public uint Iterations { get; set; }
        public uint MemorySizeKiB { get; set; } 
        public uint Parallelism { get; set; }
        public uint DerivedKeyLength { get; set; }

        public Argon2Result(string hash, string salt, Argon2Variant variant, uint iterations, uint memorySizeKiB, uint parallelism, uint derivedKeyLength)
        {
            Hash = hash;
            Salt = salt;
            Variant = variant;
            Iterations = iterations;
            MemorySizeKiB = memorySizeKiB;
            Parallelism = parallelism;
            DerivedKeyLength = derivedKeyLength;
        }

        public static Argon2Result Deserialize(string json)
        {
            Argon2Result? obj = JsonSerializer.Deserialize<Argon2Result>(json);
            return obj == null ? throw new DeserializeException(typeof(Argon2Result), json) : obj;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false });
        }

        public byte[] RawSalt { get { return HashManager.FromBase64(Salt); } }
        public byte[] RawHash { get { return HashManager.FromBase64(Hash); } }
    }
}
