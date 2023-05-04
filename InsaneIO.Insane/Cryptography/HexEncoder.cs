using InsaneIO.Insane.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class HexEncoder : IEncoder
    {
        public static Type SelfType => typeof(HexEncoder);
        public string Name { get => IBaseSerialize.GetName(SelfType); }

        public static readonly HexEncoder DefaultInstance = new();
        public bool ToUpper { get; init; } = false;


        public HexEncoder()
        {
        }

        public byte[] Decode(string data)
        {
            return data.FromHex();
        }

        public string Encode(byte[] data)
        {
            return data.ToHex(ToUpper);
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
                [nameof(ToUpper)] = ToUpper
            };
        }

        public string Serialize(bool indented = false)
        {
            return ToJsonObject().ToJsonString(IJsonSerialize.GetIndentOptions(indented));
        }

        public static IEncoder Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            return new HexEncoder
            {
                ToUpper = jsonNode[nameof(ToUpper)]!.GetValue<bool>()
            };
        }
    }
}
