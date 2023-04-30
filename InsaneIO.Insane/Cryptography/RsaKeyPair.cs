using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class RsaKeyPair:IJsonSerialize
    {
        public string PublicKey { get; init; } = null!;
        public string PrivateKey { get; init; } = null!;
        public string Name { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public static RsaKeyPair? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<RsaKeyPair>(json);
        }

        public string Serialize()
        {
            return ToJsonObject().ToJsonString(new JsonSerializerOptions { WriteIndented = false, IgnoreReadOnlyProperties = true });
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
