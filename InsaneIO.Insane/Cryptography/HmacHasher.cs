using InsaneIO.Insane.Cryptography;
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
        public static Type HasherType => typeof(HmacHasher);

        public HashAlgorithm HashAlgorithm { get; init; } = HashAlgorithm.Sha512;
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;

        public string KeyString { get => Encoder.Encode(Key); init => Key =  value.ToByteArrayUtf8(); }

        public byte[] KeyBytes { get => Key; init => Key = value; }

        private byte[] Key = RandomExtensions.Next(HashExtensions.HmacKeySize);

        private string _name = IHasher.GetName(HasherType);
        public string Name
        {
            get
            {
                return _name;
            }
            init
            {
                if (_name is not null)
                {
                    return;
                }
                _name = value;
            }
        }

        public HmacHasher()
        {
        }

        public static IHasher Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
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
            return data.ToHmac(KeyBytes, HashAlgorithm);
        }

        public string ComputeEncoded(string data)
        {
            return Encoder.Encode(Compute(data.ToByteArrayUtf8()));
        }


        public string Serialize()
        {
            return ToJsonObject().ToJsonString(new JsonSerializerOptions()
            {
                WriteIndented = true
            }) ;
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
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
