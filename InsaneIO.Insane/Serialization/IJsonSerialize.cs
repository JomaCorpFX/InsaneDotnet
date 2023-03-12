using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Serialization
{
    [RequiresPreviewFeatures]
    public interface IJsonSerialize : IBaseSerialize
    {
        public JsonObject ToJsonObject();
        public string Serialize();

    }
}
