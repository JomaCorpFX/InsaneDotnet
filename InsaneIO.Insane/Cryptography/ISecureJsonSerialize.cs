using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using InsaneIO.Insane.Serialization;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface ISecureJsonSerialize: IBaseSerialize
    {
        public string Serialize(byte[] serializeKey, bool indented = false, ISecretProtector? protector = null);
        public string Serialize(string serializeKey, bool indented = false, ISecretProtector? protector = null);
        public JsonObject ToJsonObject(byte[] serializeKey, ISecretProtector? protector = null);
        public JsonObject ToJsonObject(string serializeKey, ISecretProtector? protector = null);
    }
}
