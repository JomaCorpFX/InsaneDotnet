using System.Text.Json;

namespace InsaneIO.Insane.Cryptography
{
    public class RsaKeyPair
    {
        public string PublicKey { get; init; } = null!;
        public string PrivateKey { get; init; } = null!;


        public static RsaKeyPair? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<RsaKeyPair>(json);
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false, IgnoreReadOnlyProperties = true });
        }

    }
}
