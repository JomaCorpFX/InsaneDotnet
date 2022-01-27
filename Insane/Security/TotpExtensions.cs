

namespace Insane.Extensions
{
    public static class TotpExtensions
    {
        private const long InitialCounterTime = 0;
        public const long TotpDefaultPeriod = 30;

        private static long ComputeTotpRemainingSeconds(this DateTimeOffset time, long period)
        {
            return period - (time.ToUniversalTime().ToUnixTimeSeconds() - InitialCounterTime) % period;
        }

        private static string ComputeTotpCode(this byte[] secret, TwoFactorCodeLength length, HashAlgorithm hashAlgorithm, long period)
        {
            long timeInterval = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - InitialCounterTime) / period;
            timeInterval = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(timeInterval) : timeInterval;
            byte[] hmac = BitConverter.GetBytes(timeInterval).ToHmac(secret, hashAlgorithm);
            byte offset = (byte)(hmac[19] & 0xF);
            var slice = hmac[offset..(offset + 4)];
            long code = ((BitConverter.IsLittleEndian ? BinaryPrimitives.ReadInt32BigEndian(slice) : BitConverter.ToInt32(slice)) & 0x7FFFFFFF) % (int)(Math.Pow(10, length.IntValue()));
            return code.ToString().PadLeft(length.IntValue(), '0');
        }

        private static string GenerateTotpUri(this byte[] secret, string label, string issuer, HashAlgorithm algorithm, TwoFactorCodeLength codeLength, long period)
        {
            return $"otpauth://totp/{HttpUtility.UrlEncode(label)}?secret={secret.ToBase32(true)}&issuer={HttpUtility.UrlEncode(issuer)}&algorithm={algorithm.ToString().ToUpper()}&digits={codeLength.IntValue()}&period={period}";
        }

        public static string GenerateTotpUri(this byte[] secret, string label, string issuer)
        {
            return GenerateTotpUri(secret, label, issuer, HashAlgorithm.Sha1, TwoFactorCodeLength.ValueOf6Digits, TotpDefaultPeriod);
        }

        public static string GenerateTotpUri(this string base32EncodedSecret, string label, string issuer)
        {
            return GenerateTotpUri(base32EncodedSecret.FromBase32(), label, issuer);
        }

        public static string GenerateTotpUri(this string encodedSecret, IEncoder secretDecoder, string label, string issuer)
        {
            return GenerateTotpUri(secretDecoder.Decode(encodedSecret), label, issuer);
        }


        private static bool VerifyTotpCode(this string code, byte[] secret, TwoFactorCodeLength length, HashAlgorithm hashAlgorithm, long period)
        {
            return code == ComputeTotpCode(secret, length, hashAlgorithm, period);
        }
        
        public static bool VerifyTotpCode(this string code, byte[] secret)
        {
            return VerifyTotpCode(code, secret, TwoFactorCodeLength.ValueOf6Digits, HashAlgorithm.Sha1, TotpDefaultPeriod);
        }

        public static bool VerifyTotpCode(this string code, string base32EncodedSecret)
        {
            return VerifyTotpCode(code, base32EncodedSecret.FromBase32());
        }

        public static bool VerifyTotpCode(this string code, string encodedSecret, IEncoder secretDecoder)
        {
            return VerifyTotpCode(code, secretDecoder.Decode( encodedSecret));
        }

        public static string ComputeTotpCode(this byte[] secret)
        {
            return ComputeTotpCode(secret, TwoFactorCodeLength.ValueOf6Digits, HashAlgorithm.Sha1, TotpDefaultPeriod);
        }

        public static string ComputeTotpCode(this string base32EncodedSecret)
        {
            return ComputeTotpCode(base32EncodedSecret.FromBase32());
        }

        public static string ComputeTotpCode(this string encodedSecret, IEncoder secretDecoder)
        {
            return ComputeTotpCode(secretDecoder.Decode(encodedSecret));
        }

        public static long ComputeTotpRemainingSeconds(this DateTimeOffset time)
        {
            return ComputeTotpRemainingSeconds(time, TotpDefaultPeriod);
        }

        

    }
}
