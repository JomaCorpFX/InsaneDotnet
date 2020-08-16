using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public class HashSaltPair
    {
        public HashSaltPair(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public String Hash { get; set; } = null!;
        public String Salt { get; set; } = null!;
    }
}
