using System.Runtime.Versioning;

namespace InsaneIO.Insane.Security
{
    [RequiresPreviewFeatures]
    public class TotpGenerator
    {
        public string Issuer { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public byte[] Secret { get; set; } = null!;

        public TotpGenerator(byte[] secret)
        {
            Secret = secret;
        }

        public TotpGenerator(string base32EncodedSecret)
        {
            Secret = base32EncodedSecret.FromBase32();
        }

        public TotpGenerator(string encodedSecret, IEncoder secretDecoder)
        {
            Secret = secretDecoder.Decode(encodedSecret);
        }

        public string GenerateTotpUri()
        {
            return Secret.GenerateTotpUri(Label, Issuer);
        }

        public bool VerifyTotpCode(string code)
        {
            return code.VerifyTotpCode(Secret);
        }

        public string ComputeTotpCode()
        {
            return Secret.ComputeTotpCode();
        }

        public long ComputeTotpRemainingSeconds()
        {
            return DateTimeOffset.UtcNow.ComputeTotpRemainingSeconds();
        }
    }
}
