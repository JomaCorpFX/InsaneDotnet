﻿using InsaneIO.Insane.Misc;
using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class Base64Encoder : IEncoder, IDefaultInstance<Base64Encoder>
    {
        public const uint NoLineBreaks = 0;
        public const uint MimeLineBreaksLength = 76;
        public const uint PemLineBreaksLength = 64;
        public static Type SelfType => typeof(Base64Encoder);
        public string AssemblyName { get => IJsonSerializable.GetName(SelfType); }

        public uint LineBreaksLength { get; init; } = NoLineBreaks;
        public bool RemovePadding { get; init; } = false;
        public Base64Encoding EncodingType { get; init; } = Base64Encoding.Base64;

        private static readonly Base64Encoder _DefaultInstance = new();
        public static Base64Encoder DefaultInstance => _DefaultInstance;

        public Base64Encoder()
        {
        }

        public byte[] Decode(string data)
        {
            return data.DecodeFromBase64();
        }

        public string Encode(byte[] data)
        {
            switch (EncodingType)
            {
                case Base64Encoding.Base64:
                    return data.EncodeToBase64(LineBreaksLength, RemovePadding);
                case Base64Encoding.UrlSafeBase64:
                    return data.EncodeToUrlSafeBase64();
                case Base64Encoding.FileNameSafeBase64:
                    return data.EncodeToFilenameSafeBase64();
                case Base64Encoding.UrlEncodedBase64:
                    return data.EncodeToUrlEncodedBase64();
                default:
                    throw new NotImplementedException(EncodingType.ToString());
            }
        }

        public string Encode(string data)
        {
            return Encode(data.ToByteArrayUtf8());
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(AssemblyName)] = AssemblyName,
                [nameof(LineBreaksLength)] = LineBreaksLength,
                [nameof(RemovePadding)] = RemovePadding,
                [nameof(EncodingType)] = EncodingType.NumberValue<int>()
            };
        }

        public string Serialize(bool indented)
        {
            return ToJsonObject().ToJsonString(IJsonSerializable.GetIndentOptions(indented));
        }

        public static IEncoder Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            return new Base64Encoder
            {
                EncodingType = Enum.Parse<Base64Encoding>(jsonNode[nameof(LineBreaksLength)]!.GetValue<int>().ToString()),
                LineBreaksLength = jsonNode[nameof(LineBreaksLength)]!.GetValue<uint>(),
                RemovePadding = jsonNode[nameof(RemovePadding)]!.GetValue<bool>()
            };
        }

   
    }
}
