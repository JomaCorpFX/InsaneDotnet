using Insane.Exceptions;
using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class Argon2Result: HashResultBase
    {
        public string Hash { get; init; } = null!;
        public string Salt { get; init; } = null!;
        public Argon2Variant Variant { get; init; }
        public uint Iterations { get; init; }
        public uint MemorySizeKiB { get; init; } 
        public uint Parallelism { get; init; }
        public uint DerivedKeyLength { get; init; }


        public Argon2Result(string hash, string salt, Argon2Variant variant, uint iterations, uint memorySizeKiB, uint parallelism, uint derivedKeyLength, IEncoder encoder):base(encoder)
        {
            Hash = hash;
            Salt = salt;
            Variant = variant;
            Iterations = iterations;
            MemorySizeKiB = memorySizeKiB;
            Parallelism = parallelism;
            DerivedKeyLength = derivedKeyLength;
        }
        public byte[] RawSalt { get { return Salt.FromBase64(); } }
        public byte[] RawHash { get { return Hash.FromBase64(); } }

        public static Argon2Result? Deserialize(string json, IEncoder encoder)
        {
            Argon2Result? obj = JsonSerializer.Deserialize<Argon2Result>(json);
            if (obj is not null)
            {
                obj.Encoder = encoder;
            }
            return obj;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false, IgnoreReadOnlyProperties = true });
        }

    }
}
