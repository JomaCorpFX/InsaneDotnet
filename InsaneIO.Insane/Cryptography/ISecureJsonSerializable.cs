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
    public interface ISecureJsonSerializable: IBaseSerializable
    {

        public string Serialize(byte[] serializeKey, bool indented = false);
        public string Serialize(string serializeKey, bool indented = false);
        public JsonObject ToJsonObject(byte[] serializeKey);
        public JsonObject ToJsonObject(string serializeKey);
    }
}
