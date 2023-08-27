using InsaneIO.Insane.Misc;
using InsaneIO.Insane.Serialization;
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
        public static Type SelfType => typeof(Argon2Hasher);
        public string AssemblyName { get => IJsonSerializable.GetName(SelfType); }

        public string SaltString { get => Encoder.Encode(Salt); init => Salt = value.ToByteArrayUtf8(); }

        public byte[] SaltBytes { get => Salt; init => Salt = value; }

        private byte[] Salt = RandomExtensions.NextBytes(Constants.Argon2SaltSize);

        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;
        public uint Iterations { get; init; } = Constants.Argon2Iterations;
        public uint MemorySizeKiB { get; init; } = Constants.Argon2MemorySizeInKiB;
        public uint DegreeOfParallelism { get; init; } = Constants.Argon2DegreeOfParallelism;
        public uint DerivedKeyLength { get; init; } = Constants.Argon2DerivedKeyLength;
        public Argon2Variant Argon2Variant { get; init; } = Argon2Variant.Argon2id;

        public Argon2Hasher()
        {
        }

        public static IHasher Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.AssemblyName)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            return new Argon2Hasher
            {
                Salt = encoder.Decode(jsonNode[nameof(Salt)]!.GetValue<string>()),
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
            return data.ComputeArgon2(Salt, Iterations, MemorySizeKiB, DegreeOfParallelism, Argon2Variant, DerivedKeyLength);
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
            return new JsonObject
            {
                [nameof(AssemblyName)] = AssemblyName,
                [nameof(Salt)] = Encoder.Encode(Salt),
                [nameof(Iterations)] = Iterations,
                [nameof(MemorySizeKiB)] = MemorySizeKiB,
                [nameof(DegreeOfParallelism)] = DegreeOfParallelism,
                [nameof(Argon2Variant)] = Argon2Variant.NumberValue<int>(),
                [nameof(DerivedKeyLength)] = DerivedKeyLength,
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
