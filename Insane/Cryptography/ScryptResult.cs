using Insane.Exceptions;
using Insane.Extensions;
using Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class ScryptResult:HashResultBase
    {
        public string Hash { get; init; } = null!;
        public string Salt { get; init; } = null!;
        public uint Iterations { get; init; } = 32768;
        public uint BlockSize { get; init; } = 8;
        public uint Parallelism { get; init; } = 1;
        public uint DerivedKeyLength { get; init; } = 64;

        public byte[] RawSalt { get { return Salt.FromBase64(); } }
        public byte[] RawHash { get { return Hash.FromBase64(); } }

        public ScryptResult(string hash, string salt, uint iterations, uint blockSize, uint parallelism, uint derivedKeyLength, IEncoder encoder):base(encoder)
        {
            Hash = hash;
            Salt = salt;
            Iterations = iterations;
            BlockSize = blockSize;
            Parallelism = parallelism;
            DerivedKeyLength = derivedKeyLength;
        }

        public static ScryptResult? Deserialize(string json, IEncoder encoder)
        {
            ScryptResult? obj = JsonSerializer.Deserialize<ScryptResult>(json);
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
