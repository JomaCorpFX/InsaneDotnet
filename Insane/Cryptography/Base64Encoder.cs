using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Cryptography
{

    public class Base64Encoder : IEncoder
    {
        public const int NoLineBreaks = 0;
        public const int MimeLineBreaksLength = 76;
        public const int PemLineBreaksLength = 64;

        public uint LineBreaksLength { get; set; } = NoLineBreaks;
        public bool RemovePadding { get; set; } = false;
        public Base64Encoding EncodingType { get; set; } = Base64Encoding.Base64;

        public static readonly Base64Encoder Instance = new Base64Encoder();

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
                case Base64Encoding.UrlSafeBase64:
                    return data.ToUrlSafeBase64();
                case Base64Encoding.FileNameSafeBase64:
                    return data.ToFilenameSafeBase64();
                case Base64Encoding.UrlEncodedBase64:
                    return data.ToUrlEncodedBase64();
                default:
                    return data.ToBase64(LineBreaksLength, RemovePadding);
            }
        }

    }
}
