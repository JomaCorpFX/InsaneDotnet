using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class Base32Encoder : IEncoder
    {
        public static Type EncoderType => typeof(Base32Encoder);

        public static readonly Base32Encoder DefaultInstance = new Base32Encoder();
        public bool ToLower { get; set; } = false;
        public bool RemovePadding { get; set; } = false;

        private string _name = IBaseSerialize.GetName(EncoderType);
        public string Name
        {
            get
            {
                return _name;
            }
            init
            {
                if (_name is not null)
                {
                    return;
                }
                _name = value;
            }
        }

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

        public string Serialize()
        {
            return ToJsonObject().ToJsonString();
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
