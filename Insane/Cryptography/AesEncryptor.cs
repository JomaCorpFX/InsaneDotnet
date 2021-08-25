using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public class AesEncryptor : IEncryptor
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string Key { get; set; }

        public IEncoder Encoder { get; set; } 

        public AesEncryptor(string key, IEncoder encoder)
        {
            Key = key;
            Encoder = encoder;
        }

        public string Decrypt(string data)
        {
            return data.DecryptAes(Key, Encoder);
        }

        public string Encrypt(string data)
        {
            return data.EncryptAes(Key, Encoder);
        }
    }
}
