using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class Base32Encoder : IEncoder
    {
        public static Type SelfType => typeof(Base32Encoder);
        public string Name { get => IBaseSerialize.GetName(SelfType); }

        public static readonly Base32Encoder DefaultInstance = new Base32Encoder();
        public bool ToLower { get; set; } = false;
        public bool RemovePadding { get; set; } = false;


        public Base32Encoder()
        {
        }

        public  byte[] Decode(string data)
        {
            return data.FromBase32();
        }

        public  string Encode(byte[] data)
        {
            return data.ToBase32(RemovePadding, ToLower);
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
                [nameof(RemovePadding)] = RemovePadding,
                [nameof(ToLower)] = ToLower,
            };
        }

        public string Serialize(bool indented = false)
        {
            return ToJsonObject().ToJsonString(IJsonSerialize.GetIndentOptions(indented));
        }

        public static IEncoder Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            return new Base32Encoder
            {
                RemovePadding = jsonNode[nameof(RemovePadding)]!.GetValue<bool>(),
                ToLower = jsonNode[nameof(ToLower)]!.GetValue<bool>()
            };
        }
    }
}
