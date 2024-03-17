using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    
    public class RsaKeyPair:IJsonSerializable
    {
        public RsaKeyPair(string publickey, string privatekey)
        {
            PublicKey = publickey;
            PrivateKey = privatekey;
        }

        public static Type SelfType => typeof(RsaKeyPair);
        public string AssemblyName { get => IBaseSerializable.GetName(SelfType); }
        public  string PublicKey { get; init; } = null!;
        public  string PrivateKey { get; init; } = null!;

        public static RsaKeyPair? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<RsaKeyPair>(json);
        }

        public string Serialize(bool indented = false)
        {
            return ToJsonObject().ToJsonString(IJsonSerializable.GetIndentOptions(indented));
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(AssemblyName)] = AssemblyName,
                [nameof(PublicKey)] = PublicKey,
                [nameof(PrivateKey)] = PrivateKey
            };
        }
    }
}
