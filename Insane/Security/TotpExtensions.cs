

namespace Insane.Extensions
{
    public static class TotpExtensions
    {
        private const long InitialCounterTime = 0;

        private static long ComputeTotpRemainingSeconds(this DateTimeOffset time, TotpPeriod period)
        {
            return period.IntValue() - (time.ToUniversalTime().ToUnixTimeSeconds() - InitialCounterTime) % period.IntValue();
        }

        private static string ComputeTotpCode(this byte[] secret, TwoFactorCodeLength length, HashAlgorithm hashAlgorithm, TotpPeriod period)
        {
            long timeInterval = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - InitialCounterTime) / period.IntValue();
            timeInterval = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(timeInterval) : timeInterval;
            byte[] hmac = BitConverter.GetBytes(timeInterval).ToHmac(secret, hashAlgorithm);
            byte offset = (byte)(hmac[19] & 0xF);
            var slice = hmac[offset..(offset + 4)];
            long code = ((BitConverter.IsLittleEndian ? BinaryPrimitives.ReadInt32BigEndian(slice) : BitConverter.ToInt32(slice)) & 0x7FFFFFFF) % (int)(Math.Pow(10, length.IntValue()));
            return code.ToString().PadLeft(length.IntValue(), '0');
        }

        private static string GenerateTotpUri(this byte[] secret, string label, string issuer, HashAlgorithm algorithm, TwoFactorCodeLength codeLength, TotpPeriod period)
        {
            return $"otpauth://totp/{HttpUtility.UrlEncode(label)}?secret={secret.ToBase32(true)}&issuer={HttpUtility.UrlEncode(issuer)}&algorithm={algorithm.ToString().ToUpper()}&digits={codeLength.IntValue()}&period={period.IntValue()}";
        }

        public static string GenerateTotpUri(this byte[] secret, string label, string issuer)
        {
            return GenerateTotpUri(secret, label, issuer, HashAlgorithm.Sha1, TwoFactorCodeLength.ValueOf6Digits, TotpPeriod.ValueOf30Seconds);
        }

        public static string GenerateTotpUri(this string secret, string label, string issuer)
        {
            return GenerateTotpUri(secret.FromBase32(), label, issuer);
        }

        public static string GenerateTotpUri(this string secret, IEncoder secretDecoder, string label, string issuer)
        {
            return GenerateTotpUri(secretDecoder.Decode(secret), label, issuer);
        }


        private static bool VerifyTotpCode(this string code, byte[] secret, TwoFactorCodeLength length, HashAlgorithm hashAlgorithm, TotpPeriod period)
        {
            return code == ComputeTotpCode(secret, length, hashAlgorithm, period);
        }
        
        public static bool VerifyTotpCode(this string code, byte[] secret)
        {
            return VerifyTotpCode(code, secret, TwoFactorCodeLength.ValueOf6Digits, HashAlgorithm.Sha1, TotpPeriod.ValueOf30Seconds);
        }

        public static bool VerifyTotpCode(this string code, string secret)
        {
            return VerifyTotpCode(code, secret.FromBase32());
        }

        public static bool VerifyTotpCode(this string code, string secret, IEncoder secretDecoder)
        {
            return VerifyTotpCode(code, secretDecoder.Decode( secret));
        }

        public static string ComputeTotpCode(this byte[] secret)
        {
            return ComputeTotpCode(secret, TwoFactorCodeLength.ValueOf6Digits, HashAlgorithm.Sha1, TotpPeriod.ValueOf30Seconds);
        }

        public static string ComputeTotpCode(this string secret)
        {
            return ComputeTotpCode(secret.FromBase32());
        }

        public static string ComputeTotpCode(this string secret, IEncoder secretDecoder)
        {
            return ComputeTotpCode(secretDecoder.Decode(secret));
        }

        public static long ComputeTotpRemainingSeconds(this DateTimeOffset time)
        {
            return ComputeTotpRemainingSeconds(time, TotpPeriod.ValueOf30Seconds);
        }

        

    }
}
