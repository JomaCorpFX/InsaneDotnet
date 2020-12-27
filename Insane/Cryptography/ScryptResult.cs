using Insane.Exceptions;
using Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class ScryptResult
    {
        public string Hash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public uint Iterations { get; set; } = 32768;
        public uint BlockSize { get; set; } = 8;
        public uint Parallelism { get; set; } = 1;
        public uint DerivedKeyLength { get; set; } = 64;

        public ScryptResult(string hash, string salt, uint iterations, uint blockSize, uint parallelism, uint derivedKeyLength)
        {
            Hash = hash;
            Salt = salt;
            Iterations = iterations;
            BlockSize = blockSize;
            Parallelism = parallelism;
            DerivedKeyLength = derivedKeyLength;
        }

        public static ScryptResult Deserialize(string json)
        {
            ScryptResult? obj = JsonSerializer.Deserialize<ScryptResult>(json);
            return obj == null ? throw new DeserializeException(typeof(ScryptResult)) : obj;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false });
        }

        public byte[] RawSalt { get { return HashManager.FromBase64(Salt); } }
        public byte[] RawHash { get { return HashManager.FromBase64(Hash); } }

    }
}
