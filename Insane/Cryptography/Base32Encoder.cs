using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{
    public class Base32Encoder : IEncoder
    {
        public static readonly Base32Encoder Instance = new Base32Encoder();
        public bool ToLower { get; set; } = false;
        public bool RemovePadding { get; set; } = false;

        public Base32Encoder(bool toLower=false)
        {
            
        }

        public byte[] Decode(string data)
        {
            return data.FromBase32();
        }

        public string Encode(byte[] data)
        {
            return ToLower ? data.ToBase32(RemovePadding).ToLower() : data.ToBase32(RemovePadding);
        }
    }
}
