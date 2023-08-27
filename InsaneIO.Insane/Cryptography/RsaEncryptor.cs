using InsaneIO.Insane.Extensions;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;
using InsaneIO.Insane.Serialization;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class RsaEncryptor : IEncryptor
    {
        public static Type SelfType => typeof(RsaEncryptor);
        public string AssemblyName { get => IBaseSerializable.GetName(SelfType); }

        public required RsaKeyPair KeyPair { get; init; }
        public RsaPadding Padding { get; init; } = RsaPadding.OaepSha256;
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;

        public ISecretProtector Protector { get; init; } = new AesCbcProtector();

        public RsaEncryptor()
        {
        }

        public byte[] Encrypt(byte[] data)
        {
            return data.EncryptRsa(KeyPair.PublicKey, Padding);
        }

        public string EncryptEncoded(string data)
        {
            return Encoder.Encode(Encrypt(data.ToByteArrayUtf8()));
        }

        public byte[] Decrypt(byte[] data)
        {
            return data.DecryptRsa(KeyPair.PrivateKey, Padding);
        }

        public string DecryptEncoded(string data)
        {
            return Decrypt(Encoder.Decode(data)).ToStringUtf8();
        }

        public static IEncryptor Deserialize(string json, byte[] serializeKey)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.AssemblyName)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            Type protectorType = Type.GetType(jsonNode[nameof(Protector)]!.GetValue<string>())!;
            ISecretProtector protector = (ISecretProtector)Activator.CreateInstance(protectorType)!;
            string publickey = jsonNode[nameof(KeyPair)]![nameof(RsaKeyPair.PublicKey)]!.GetValue<string?>()!;
            string privatekey = jsonNode[nameof(KeyPair)]![nameof(RsaKeyPair.PrivateKey)]!.GetValue<string?>()!;
            RsaKeyPair keyPair = new RsaKeyPair
            {
                PublicKey = protector.Unprotect(encoder.Decode(publickey), serializeKey).ToStringUtf8(),
                PrivateKey = protector.Unprotect(encoder.Decode(privatekey), serializeKey).ToStringUtf8()
            };
            return new RsaEncryptor
            {
                KeyPair = keyPair,
                Encoder = encoder,
                Padding = Enum.Parse<RsaPadding>(jsonNode[nameof(Padding)]!.GetValue<int>().ToString())
            };
        }

        public static IEncryptor Deserialize(string json, string serializeKey)
        {
            return Deserialize(json, serializeKey.ToByteArrayUtf8());
        }

        public string Serialize(byte[] serializeKey, bool indented = false)
        {
            return ToJsonObject(serializeKey).ToJsonString(IJsonSerializable.GetIndentOptions(indented));
        }

        public string Serialize(string serializeKey, bool indented = false)
        {
            return ToJsonObject(serializeKey).ToJsonString(IJsonSerializable.GetIndentOptions(indented));
        }

        public JsonObject ToJsonObject(byte[] serializeKey)
        {
            return new JsonObject
            {
                [nameof(AssemblyName)] = AssemblyName,
                [nameof(Protector)] = Protector.AssemblyName,
                [nameof(KeyPair)] = (new RsaKeyPair
                {
                    PublicKey = Encoder.Encode(Protector.Protect(KeyPair.PublicKey.ToByteArrayUtf8(), serializeKey)),
                    PrivateKey = Encoder.Encode(Protector.Protect(KeyPair.PrivateKey.ToByteArrayUtf8(), serializeKey))
                }).ToJsonObject(),
                [nameof(Padding)] = Padding.NumberValue<int>(),
                [nameof(Encoder)] = Encoder.ToJsonObject(),

            };
        }

        public JsonObject ToJsonObject(string serializeKey)
        {
            return ToJsonObject(serializeKey.ToByteArrayUtf8());
        }
    }
}
