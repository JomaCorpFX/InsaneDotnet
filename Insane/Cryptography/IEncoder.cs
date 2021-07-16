using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public interface IEncoder
    {
        public sealed string Name()
        {
            return GetType().Name;
        }

        public string Encode(byte[] data);
        public byte[] Decode(string data);

    }
}
