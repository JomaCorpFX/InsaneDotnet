using Insane.Cryptography;
using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    
    public class HexEncoder : IEncoder
    {
        public static readonly HexEncoder Instance = new HexEncoder();

        public byte[] Decode(string data)
        {
            return data.FromHex();
        }

        public string Encode(byte[] data)
        {
            return data.ToHex();
        }

    }
}
