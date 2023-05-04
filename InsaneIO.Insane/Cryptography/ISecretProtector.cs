using InsaneIO.Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface ISecretProtector: IBaseSerialize
    {
        public byte[] Protect(byte[] secret, byte[] key);
        public byte[] Unprotect(byte[] secret, byte[] key);
    }
}
