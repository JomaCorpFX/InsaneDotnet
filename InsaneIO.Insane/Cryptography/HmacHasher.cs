using InsaneIO.Insane.Cryptography;
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
        public string Key { get; set; } = Base64Encoder.DefaultInstance.Encode(RandomManager.Next(HashExtensions.Sha512HashSizeInBytes));

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
            return new HmacHasher
            {
                HashAlgorithm = Enum.Parse<HashAlgorithm>(jsonNode[nameof(HashAlgorithm)]!.GetValue<int>().ToString()),
                Encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!,
                Key = jsonNode[nameof(Key)]!.GetValue<string>()
            };
        }

        public byte[] Compute(byte[] data)
        {
            return data.ToHmac(Key.ToByteArrayUtf8(), HashAlgorithm);
        }

        public string Compute(string data)
        {
            return Encoder.Encode(Compute(data.ToByteArrayUtf8()));
        }


        public string Serialize()
        {
            return ToJsonObject().ToJsonString();
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject()
            {
                [nameof(Name)] = Name,
                [nameof(HashAlgorithm)] = HashAlgorithm.NumberValue<int>(),
                [nameof(Encoder)] = Encoder.ToJsonObject(),
                [nameof(Key)] = Key
            };
        }

        public bool Verify(byte[] data, byte[] expected)
        {
            return Enumerable.SequenceEqual(Compute(data), expected);
        }

        public bool Verify(string data, string expected)
        {
            return Compute(data).Equals(expected);
        }
    }
}
