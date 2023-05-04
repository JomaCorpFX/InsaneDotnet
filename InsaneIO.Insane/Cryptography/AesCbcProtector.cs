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
    public class AesCbcProtector : ISecretProtector
    {
        public static Type SelfType => typeof(AesCbcProtector);
        public string Name { get => IBaseSerialize.GetName(SelfType); }

        public byte[] Protect(byte[] secret, byte[] key)
        {
            return AesExtensions.EncryptAesCbc(secret, key,AesCbcPadding.Pkcs7);
        }

        public byte[] Unprotect(byte[] secret, byte[] key)
        {
            return AesExtensions.DecryptAesCbc(secret, key, AesCbcPadding.Pkcs7);
        }
    }
}
