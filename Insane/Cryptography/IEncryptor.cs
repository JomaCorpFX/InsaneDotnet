using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public interface IEncryptor
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}
