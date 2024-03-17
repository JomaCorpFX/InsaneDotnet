using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    
    public interface IHasher: IHasherJsonSerializable
    {

        public byte[] Compute(byte[] data);
        public byte[] Compute(string data);
        public string ComputeEncoded(byte[] data);
        public string ComputeEncoded(string data);

        public bool Verify(byte[] data, byte[] expected);
        public bool Verify(string data, byte[] expected);
        public bool VerifyEncoded(byte[] data, string expected);
        public bool VerifyEncoded(string data, string expected);
    }
}
