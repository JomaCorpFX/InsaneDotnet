using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    public interface ISecureJsonSerialize: IBaseSerialize
    {
        public string Serialize(byte[] serializeKey);
        public string Serialize(string serializeKey);
        public JsonObject ToJsonObject(byte[] serializeKey);
        public JsonObject ToJsonObject(string serializeKey);
    }
}
