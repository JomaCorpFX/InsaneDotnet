using Insane.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Insane.Cryptography
{
    public class HmacResult
    {
        public string Hash { get; set; } = null!;
        public string Key { get; set; } = null!;
        public HashAlgorithm Algorithm { get; set; } = HashAlgorithm.Sha512;

        public byte[] RawKey { get { return HashManager.FromBase64(Key); } }
        public byte[] RawHash { get { return HashManager.FromBase64(Hash); } }

        public HmacResult(string hash, string key, HashAlgorithm algorithm)
        {
            Hash = hash;
            Key = key;
            Algorithm = algorithm;
        }

        public static HmacResult Deserialize(string json)
        {
            HmacResult? obj = JsonSerializer.Deserialize<HmacResult>(json);
            return obj == null ? throw new DeserializeException(typeof(HmacResult), json) : obj;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false });
        }
    }
}
