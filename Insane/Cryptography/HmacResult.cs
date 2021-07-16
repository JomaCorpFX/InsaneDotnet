using Insane.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class HmacResult : HashResultBase
    {
        public string Hash { get; init; } = null!;
        public string Key { get; init; } = null!;
        public HashAlgorithm Algorithm { get; init; } = HashAlgorithm.Sha512;

        public byte[] RawKey { get { return Encoder.Decode(Key); } }
        public byte[] RawHash { get { return Encoder.Decode(Hash); } }


        public HmacResult(string hash, string key, HashAlgorithm algorithm, IEncoder encoder):base(encoder)
        {
            Hash = hash;
            Key = key;
            Algorithm = algorithm;
        }

        public static HmacResult? Deserialize(string json, IEncoder encoder)
        {
            HmacResult? obj = JsonSerializer.Deserialize<HmacResult>(json);
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
