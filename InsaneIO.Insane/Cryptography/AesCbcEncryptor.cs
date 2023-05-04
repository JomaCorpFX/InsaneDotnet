using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{


    [RequiresPreviewFeatures]
    public class AesCbcEncryptor : IEncryptor, IEncryptorJsonSerialize
    {
        public static Type SelfType => typeof(AesCbcEncryptor);
        public string Name { get => IBaseSerialize.GetName(SelfType); }


        public string KeyString { get => Encoder.Encode(Key); init => Key = value.ToByteArrayUtf8(); }

        public byte[] KeyBytes { get => Key; init => Key = value; }

        private byte[] Key = RandomExtensions.Next(AesExtensions.MaxKeyLength);

        public IEncoder Encoder { get; set; } = Base64Encoder.DefaultInstance;
        public AesCbcPadding Padding { get; set; } = AesCbcPadding.Pkcs7;

        public AesCbcEncryptor()
        {
        }

        public byte[] Decrypt(byte[] data)
        {
            return data.DecryptAesCbc(Key, Padding);
        }

        public string DecryptEncoded(string data)
        {
            return Decrypt(Encoder.Decode(data)).ToStringFromUtf8();
        }
        public byte[] Encrypt(byte[] data)
        {
            return data.EncryptAesCbc(Key, Padding);
        }

        public string EncryptEncoded(string data)
        {
            return Encoder.Encode(Encrypt(data.ToByteArrayUtf8()));
        }

        public static IEncryptor Deserialize(string json, byte[] serializeKey)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            Type protectorType = Type.GetType(jsonNode["Protector"]!.GetValue<string>())!;
            ISecretProtector protector = (ISecretProtector)Activator.CreateInstance(protectorType)!;
            return new AesCbcEncryptor {
                Key = protector.Unprotect(encoder.Decode(jsonNode[nameof(Key)]!.GetValue<string>()!), serializeKey),
                Encoder = encoder,
                Padding = Enum.Parse<AesCbcPadding>(jsonNode[nameof(Padding)]!.GetValue<int>().ToString())
            };
        }

        public static IEncryptor Deserialize(string json, string serializeKey)
        {
            return Deserialize(json, serializeKey.ToByteArrayUtf8());
        }

        public string Serialize(byte[] serializeKey, bool indented = false, ISecretProtector? protector = null)
        {
            return ToJsonObject(serializeKey, protector).ToJsonString(IJsonSerialize.GetIndentOptions(indented));
        }

        public string Serialize(string serializeKey, bool indented = false, ISecretProtector? protector = null)
        {
            return ToJsonObject(serializeKey, protector).ToJsonString(IJsonSerialize.GetIndentOptions(indented));
        }

        public JsonObject ToJsonObject(byte[] serializeKey, ISecretProtector? protector = null)
        {
            protector ??= new AesCbcProtector();
            return new JsonObject
            {
                [nameof(Name)] = Name,
                ["Protector"] = protector.Name,
                [nameof(Key)] = Encoder.Encode(protector.Protect(Key, serializeKey)),
                [nameof(Padding)] = Padding.NumberValue<int>(),
                [nameof(Encoder)] = Encoder.ToJsonObject(),
            };
        }

        public JsonObject ToJsonObject(string serializeKey, ISecretProtector? protector = null)
        {
            return ToJsonObject(serializeKey.ToByteArrayUtf8(), protector);
        }
    }
}
