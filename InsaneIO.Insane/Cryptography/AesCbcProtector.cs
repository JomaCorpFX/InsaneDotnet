using InsaneIO.Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class AesCbcProtector : ISecretProtector
    {
        public static Type SelfType => typeof(AesCbcProtector);
        public string AssemblyName { get => IJsonSerializable.GetName(SelfType); }

        public byte[] Protect(byte[] secret, byte[] key)
        {
            return AesExtensions.EncryptAesCbc(secret, key,AesCbcPadding.Pkcs7);
        }

        public string Serialize(bool indented = false)
        {
            throw new NotImplementedException();
        }

        public JsonObject ToJsonObject()
        {
            throw new NotImplementedException();
        }

        public byte[] Unprotect(byte[] secret, byte[] key)
        {
            return AesExtensions.DecryptAesCbc(secret, key, AesCbcPadding.Pkcs7);
        }
    }
}
