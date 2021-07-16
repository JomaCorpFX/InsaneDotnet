using Insane.Exceptions;
using Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public class RsaKeyPair
    {
        public string PublicKey { get; init; } = null!;
        public string PrivateKey { get; init; } = null!;

        private RsaKeyPair()
        {
        }

        public RsaKeyPair(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }


        public static RsaKeyPair? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<RsaKeyPair>(json);
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false, IgnoreReadOnlyProperties = true });
        }

    }
}
