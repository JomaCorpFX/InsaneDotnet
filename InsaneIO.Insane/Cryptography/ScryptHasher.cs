using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class ScryptHasher : IHasher
    {
        public static Type HasherType => typeof(ScryptHasher);

        public string Salt { get; init; } = Base64Encoder.DefaultInstance.Encode(RandomExtensions.Next(HashExtensions.ScryptSaltSize));
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;
        public uint Iterations { get; init; } = HashExtensions.ScryptIterations;
        public uint BlockSize { get; init; } = HashExtensions.ScryptBlockSize;
        public uint Parallelism { get; init; } = HashExtensions.ScryptParallelism;
        public uint DerivedKeyLength { get; init; } = HashExtensions.ScryptDerivedKeyLength;

        private string _name = IBaseSerialize.GetName(HasherType);
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

        public ScryptHasher()
        {
        }

        public static IHasher Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
            return new ScryptHasher
            {
                Salt = jsonNode[nameof(Salt)]!.GetValue<string>(),
                Encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!,
                Iterations = jsonNode[nameof(Iterations)]!.GetValue<uint>(),
                BlockSize = jsonNode[nameof(BlockSize)]!.GetValue<uint>(),
                Parallelism = jsonNode[nameof(Parallelism)]!.GetValue<uint>(),
                DerivedKeyLength = jsonNode[nameof(DerivedKeyLength)]!.GetValue<uint>()
            };
        }

        public byte[] Compute(byte[] data)
        {
            return data.ToScrypt(Salt.ToByteArrayUtf8(), Iterations, BlockSize, Parallelism, DerivedKeyLength);
        }

        public string ComputeEncoded(string data)
        {
            return data.ToScrypt(Salt, Encoder, Iterations, BlockSize, Parallelism, DerivedKeyLength);
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
                [nameof(BlockSize)] = BlockSize,
                [nameof(Parallelism)] = Parallelism,
                [nameof(DerivedKeyLength)] = DerivedKeyLength
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
