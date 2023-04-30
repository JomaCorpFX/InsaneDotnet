using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class Base64Encoder : IEncoder
    {
        public const uint NoLineBreaks = 0;
        public const uint MimeLineBreaksLength = 76;
        public const uint PemLineBreaksLength = 64;
        public static Type EncoderType => typeof(Base64Encoder);

        public uint LineBreaksLength { get; init; } = NoLineBreaks;
        public bool RemovePadding { get; init; } = false;
        public Base64Encoding EncodingType { get; init; } = Base64Encoding.Base64;

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

        public static readonly Base64Encoder DefaultInstance = new Base64Encoder();

        public Base64Encoder()
        {
        }

        public byte[] Decode(string data)
        {
            return data.FromBase64();
        }

        public string Encode(byte[] data)
        {
            switch (EncodingType)
            {
                case Base64Encoding.Base64:
                    return data.ToBase64(LineBreaksLength, RemovePadding);
                case Base64Encoding.UrlSafeBase64:
                    return data.ToUrlSafeBase64();
                case Base64Encoding.FileNameSafeBase64:
                    return data.ToFilenameSafeBase64();
                case Base64Encoding.UrlEncodedBase64:
                    return data.ToUrlEncodedBase64();
                default:
                    throw new NotImplementedException(EncodingType.ToString());
            }
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
                [nameof(LineBreaksLength)] = LineBreaksLength,
                [nameof(RemovePadding)] = RemovePadding,
                [nameof(EncodingType)] = EncodingType.NumberValue<int>()
            };
        }

        public string Serialize()
        {
            return ToJsonObject().ToJsonString();
        }

        public static IEncoder Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            return new Base64Encoder
            {
                EncodingType = Enum.Parse<Base64Encoding>(jsonNode[nameof(LineBreaksLength)]!.GetValue<int>().ToString()),
                LineBreaksLength = jsonNode[nameof(LineBreaksLength)]!.GetValue<uint>(),
                RemovePadding = jsonNode[nameof(RemovePadding)]!.GetValue<bool>()
            };
        }

   
    }
}
