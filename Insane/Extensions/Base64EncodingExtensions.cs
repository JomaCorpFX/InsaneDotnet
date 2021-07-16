using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class Base64EncodingExtensions
    {
        public static string InsertLineBreaks(this string data, uint lineBreaksLength = Base64Encoder.MimeLineBreaksLength)
        {
            int distance = (int)lineBreaksLength;
            if (lineBreaksLength == 0)
            {
                return data;
            }
            StringBuilder sb = new StringBuilder();
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
                    sb.AppendLine(data.Substring(Segments * distance));
                }
                return sb.ToString().Trim();
            }
        }

        public static string ToBase64(this byte[] data, uint lineBreaksLength = Base64Encoder.NoLineBreaks, bool removePadding = false)
        {
            return InsertLineBreaks(Convert.ToBase64String(data), lineBreaksLength).Replace("=", removePadding ? "" : "=");
        }

        public static string ToBase64(this string data, uint lineBreaksLength = Base64Encoder.NoLineBreaks, bool removePadding = false)
        {
            return ToBase64(data.ToByteArray(), lineBreaksLength, removePadding);
        }

        public static string ToUrlSafeBase64(this byte[] data)
        {
            return ToBase64(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        public static string ToFilenameSafeBase64(this byte[] data)
        {
            return ToUrlSafeBase64(data);
        }

        public static string ToUrlEncodedBase64(this byte[] data)
        {
            return ToBase64(data).Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D");
        }

        public static byte[] FromBase64(this string data)
        {
            int modulo = data.Length % 4;
            data = data.Replace("%2B", "+").Replace("%2F", "/").Replace("%3D", "=")
                .Replace("-", "+").Replace("_", "/").Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\r\n", string.Empty)
                .PadRight(data.Length + (modulo > 0 ? 4 - modulo : 0), '=');
            return Convert.FromBase64String(data);
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
