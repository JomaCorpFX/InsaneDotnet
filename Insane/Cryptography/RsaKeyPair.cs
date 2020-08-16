using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public String PublicKey { get; set; } = null!;
        public String PrivateKey { get; set; } = null!;
    }
}
