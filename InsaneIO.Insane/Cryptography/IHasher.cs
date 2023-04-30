using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface IHasher: IHasherJsonSerialize
    {
        public static abstract Type HasherType { get; }

        public byte[] Compute(byte[] data);
        public string ComputeEncoded(string data);

        public bool Verify(byte[] data, byte[] expected);
        public bool VerifyEncoded(string data, string expected);
    }
}
