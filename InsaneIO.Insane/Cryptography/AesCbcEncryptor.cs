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
        public static Type EncryptorType => typeof(AesCbcEncryptor);


        public required string Key { get; init; }
        public IEncoder Encoder { get; set; } = Base64Encoder.DefaultInstance;
        public AesCbcPadding Padding { get; set; } = AesCbcPadding.Pkcs7;


        private string _name = IBaseSerialize.GetName(EncryptorType);
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

        public AesCbcEncryptor()
        {
        }

        public byte[] Decrypt(byte[] data)
        {
            return data.DecryptAesCbc(Key.ToByteArrayUtf8(), Padding);
        }

        public string Decrypt(string data)
        {
            return Decrypt(Encoder.Decode(data)).ToStringUtf8();
        }
        public byte[] Encrypt(byte[] data)
        {
            return data.EncryptAesCbc(Key.ToByteArrayUtf8(), Padding);
        }

        public string Encrypt(string data)
        {
            return Encoder.Encode(Encrypt(data.ToByteArrayUtf8()));
        }

        public static IEncryptor Deserialize(string json, byte[] serializeKey)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            return new AesCbcEncryptor {
                Key = encoder.Decode(jsonNode[nameof(Key)]!.GetValue<string>()).DecryptAesCbc(serializeKey).ToStringUtf8(),
                Encoder = encoder,
                Padding = Enum.Parse<AesCbcPadding>(jsonNode[nameof(Padding)]!.GetValue<uint>().ToString())
            };
        }

        public static IEncryptor Deserialize(string json, string serializeKey)
        {
            return Deserialize(json, serializeKey.ToByteArrayUtf8());
        }

        public string Serialize(byte[] serializeKey)
        {
            return ToJsonObject(serializeKey).ToJsonString();
        }

        public string Serialize(string serializeKey)
        {
            return ToJsonObject(serializeKey).ToJsonString();
        }

        public JsonObject ToJsonObject(byte[] serializeKey)
        {
            return new JsonObject
            {
                [nameof(Name)] = Name,
                [nameof(Key)] = Encoder.Encode(Key.ToByteArrayUtf8().EncryptAesCbc(serializeKey)),
                [nameof(Encoder)] = Encoder.ToJsonObject(),
                [nameof(Padding)] = Padding.NumberValue<int>()
            };
        }

        public JsonObject ToJsonObject(string serializeKey)
        {
            return ToJsonObject(serializeKey.ToByteArrayUtf8());
        }
    }
}
