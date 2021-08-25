using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Insane.Cryptography
{
    public class RsaEncryptor : IEncryptor
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public RsaKeyPair KeyPair { get; set; }

        public IEncoder Encoder { get; set; }

        public RsaEncryptor(RsaKeyPair keyPair, IEncoder encoder) 
        {
            this.KeyPair = keyPair;
            this.Encoder = encoder;
        }

        public string Decrypt(string data)
        {
            
            return data.DecryptRsa(KeyPair.PrivateKey, Encoder);
        }

        public string Encrypt(string data)
        {
            return data.EncryptRsa(KeyPair.PublicKey, Encoder);
        }
    }
}
