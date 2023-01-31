using InsaneIO.Insane.Strings;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
    public static class Base64EncodingExtensions
    {
        public const uint NoLineBreaks = 0;
        public const uint MimeLineBreaksLength = 76;
        public const uint PemLineBreaksLength = 64;

        public static string InsertLineBreaks(this string data, uint lineBreaksLength = MimeLineBreaksLength)
        {
            int distance = (int)lineBreaksLength;
            if (lineBreaksLength == 0)
            {
                return data;
            }
            StringBuilder sb = new();
            int Segments = data.Length / distance;
            if (Segments < 0)
            {
                return data.Trim();
            }
            else
            {
                for (int i = 0; i < Segments; i++)
                {
                    sb.AppendLine(data.Substring(i * distance, distance));
                }
                if (Segments * lineBreaksLength < data.Length)
                {
                    sb.AppendLine(data[(Segments * distance)..]);
                }
                return sb.ToString().Trim();
            }
        }

        public static string ToBase64(this byte[] data, uint lineBreaksLength = NoLineBreaks, bool removePadding = false)
        {
            var result = InsertLineBreaks(Convert.ToBase64String(data), lineBreaksLength);
            return removePadding ? result.Replace(StringsConstants.EqualSign, string.Empty) : result;
        }

        public static string ToBase64(this string data, uint lineBreaksLength = NoLineBreaks, bool removePadding = false)
        {
            return ToBase64(data.ToByteArrayUtf8(), lineBreaksLength, removePadding);
        }

        public static string ToUrlSafeBase64(this byte[] data)
        {
            StringBuilder sb = new(ToBase64(data));
            sb.Replace(StringsConstants.PlusSign, StringsConstants.MinusSign).Replace(StringsConstants.Slash, StringsConstants.Underscore).Replace(StringsConstants.EqualSign, string.Empty);
            return sb.ToString();
        }

        public static string ToFilenameSafeBase64(this byte[] data)
        {
            return ToUrlSafeBase64(data);
        }

        public static string ToUrlEncodedBase64(this byte[] data)
        {
            StringBuilder sb = new(ToBase64(data));
            sb.Replace(StringsConstants.PlusSign, StringsConstants.UrlEncodedPlusSign).Replace(StringsConstants.Slash, StringsConstants.UrlEncodedSlash).Replace(StringsConstants.EqualSign, StringsConstants.UrlEncodedEqualSign);
            return sb.ToString();
        }

        public static byte[] FromBase64(this string data)
        {
            StringBuilder sb = new(data.Trim());
            sb.Replace(StringsConstants.UrlEncodedPlusSign, StringsConstants.PlusSign).Replace(StringsConstants.UrlEncodedSlash, StringsConstants.Slash).Replace(StringsConstants.UrlEncodedEqualSign, StringsConstants.EqualSign)
                .Replace(StringsConstants.MinusSign, StringsConstants.PlusSign).Replace(StringsConstants.Underscore, StringsConstants.Slash)
                .Replace(StringsConstants.LineFeed, string.Empty).Replace(StringsConstants.CarriageReturn, string.Empty);
            int modulo = sb.Length % 4;
            sb.Append('=', modulo > 0 ? 4 - modulo : 0);
            return Convert.FromBase64String(sb.ToString());
        }

        public static string Base64ToUrlSafeBase64(this string base64)
        {
            return ToUrlSafeBase64(FromBase64(base64));
        }

        public static string Base64ToFilenameSafeBase64(this string base64)
        {
            return Base64ToUrlSafeBase64(base64);
        }

        public static string Base64ToUrlEncodedBase64(this string base64)
        {
            return ToUrlEncodedBase64(FromBase64(base64));
        }
    }
}
