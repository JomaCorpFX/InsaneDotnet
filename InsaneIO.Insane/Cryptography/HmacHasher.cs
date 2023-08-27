using InsaneIO.Insane.Cryptography;
using InsaneIO.Insane.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class HmacHasher : IHasher
    {
        public static Type SelfType => typeof(HmacHasher);
        public string AssemblyName { get => IJsonSerializable.GetName(SelfType); }

        public HashAlgorithm HashAlgorithm { get; init; } = HashAlgorithm.Sha512;
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;

        public string KeyString { get => Encoder.Encode(Key); init => Key =  value.ToByteArrayUtf8(); }

        public byte[] KeyBytes { get => Key; init => Key = value; }

        private byte[] Key = RandomExtensions.NextBytes(Constants.HmacKeySize);

        public HmacHasher()
        {
        }

        public static IHasher Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.AssemblyName)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            return new HmacHasher
            {
                HashAlgorithm = Enum.Parse<HashAlgorithm>(jsonNode[nameof(HashAlgorithm)]!.GetValue<int>().ToString()),
                Encoder = encoder,
                Key = encoder.Decode( jsonNode[nameof(Key)]!.GetValue<string>())
            };
        }

        public byte[] Compute(byte[] data)
        {
            return data.ComputeHmac(KeyBytes, HashAlgorithm);
        }

        public string ComputeEncoded(string data)
        {
            return Encoder.Encode(Compute(data.ToByteArrayUtf8()));
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
                [nameof(Key)] = Encoder.Encode(Key),
                [nameof(HashAlgorithm)] = HashAlgorithm.NumberValue<int>(),
                [nameof(Encoder)] = Encoder.ToJsonObject(),
            };
        }

        public bool Verify(byte[] data, byte[] expected)
        {
            return Enumerable.SequenceEqual(Compute(data), expected);
        }

        public bool VerifyEncoded(string data, string expected)
        {
            return ComputeEncoded(data).Equals(expected);
        }
    }
}
