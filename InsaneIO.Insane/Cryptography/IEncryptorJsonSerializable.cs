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
    public interface IEncryptorJsonSerializable: ISecureJsonSerializable
    {
        public static abstract IEncryptor Deserialize(string json, byte[] serializeKey);
        public static abstract IEncryptor Deserialize(string json, string serializeKey);
    }
}
