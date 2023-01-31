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
    public class Argon2Hasher : IHasher
    {
        public static Type HasherType => typeof(Argon2Hasher);

        public string Salt { get; init; } = Base64Encoder.DefaultInstance.Encode(RandomManager.Next(HashExtensions.Argon2SaltSize));
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;
        public uint Iterations { get; init; } = HashExtensions.Argon2Iterations;
        public uint MemorySizeKiB { get; init; } = HashExtensions.Argon2MemorySizeInKiB;
        public uint DegreeOfParallelism { get; init; } = HashExtensions.Argon2DegreeOfParallelism;
        public uint DerivedKeyLength { get; init; } = HashExtensions.Argon2DerivedKeyLength;
        public Argon2Variant Argon2Variant { get; init; } = Argon2Variant.Argon2id;

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

        public Argon2Hasher()
        {
        }

        public static IHasher Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
            return new Argon2Hasher
            {
                Salt = jsonNode[nameof(Salt)]!.GetValue<string>(),
                Encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!,
                Iterations = jsonNode[nameof(Iterations)]!.GetValue<uint>(),
                MemorySizeKiB = jsonNode[nameof(MemorySizeKiB)]!.GetValue<uint>(),
                DegreeOfParallelism = jsonNode[nameof(DegreeOfParallelism)]!.GetValue<uint>(),
                DerivedKeyLength = jsonNode[nameof(DerivedKeyLength)]!.GetValue<uint>(),
                Argon2Variant = Enum.Parse<Argon2Variant>(jsonNode[nameof(Argon2Variant)]!.GetValue<uint>().ToString())
            };
        }

        public byte[] Compute(byte[] data)
        {
            return data.ToArgon2(Salt.ToByteArrayUtf8(), Iterations, MemorySizeKiB, DegreeOfParallelism, Argon2Variant, DerivedKeyLength);
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
            return new JsonObject
            {
                [nameof(Name)] = Name,
                [nameof(Salt)] = Salt,
                [nameof(Encoder)] = Encoder.ToJsonObject(),
                [nameof(Iterations)] = Iterations,
                [nameof(MemorySizeKiB)] = MemorySizeKiB,
                [nameof(DegreeOfParallelism)] = DegreeOfParallelism,
                [nameof(DerivedKeyLength)] = DerivedKeyLength,
                [nameof(Argon2Variant)] = Argon2Variant.NumberValue<int>()
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
