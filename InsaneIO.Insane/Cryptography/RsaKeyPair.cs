using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class RsaKeyPair:IJsonSerialize
    {
        public static Type SelfType => typeof(RsaKeyPair);
        public string Name { get => IBaseSerialize.GetName(SelfType); }
        public string PublicKey { get; init; } = null!;
        public string PrivateKey { get; init; } = null!;

        public static RsaKeyPair? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<RsaKeyPair>(json);
        }

        public string Serialize(bool indented = false)
        {
            return ToJsonObject().ToJsonString(IJsonSerialize.GetIndentOptions(indented));
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
                [nameof(PublicKey)] = PublicKey,
                [nameof(PrivateKey)] = PrivateKey
            };
        }
    }
}
