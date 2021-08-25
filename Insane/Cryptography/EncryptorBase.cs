using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public abstract class EncryptorBase : IEncryptor
    {
        protected readonly string key;
        protected readonly IEncoder encoder;

        protected EncryptorBase(string key, IEncoder encoder)
        {
            this.key = key;
            this.encoder = encoder;
        }

        public abstract string Decrypt(string data);
        public abstract string Encrypt(string data);
    }
}
