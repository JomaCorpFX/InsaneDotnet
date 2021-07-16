using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public class HashResultBase
    {
        [JsonIgnore]
        public IEncoder Encoder { get; set; } = Base64Encoder.Instance;

        public string EncoderName { get; private set; } 

        private HashResultBase()
        {
            EncoderName = Encoder.Name();
        }

        public HashResultBase(IEncoder encoder)
        {
            Encoder = encoder!;
            EncoderName = encoder?.Name()!;
        }
    }
}
