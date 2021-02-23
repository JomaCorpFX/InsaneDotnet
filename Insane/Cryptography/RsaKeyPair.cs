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
        public RsaKeyPair(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            
        }

        public string PublicKey { get; set; } = null!;
        public string PrivateKey { get; set; } = null!;

        public static RsaKeyPair Deserialize(string json)
        {
            RsaKeyPair? obj = JsonSerializer.Deserialize<RsaKeyPair>(json);
            return obj == null? throw new DeserializeException(typeof(RsaKeyPair)) : obj;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false});
        }
       
    }
}
