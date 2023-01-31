using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Cryptography;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public class RsaEncryptor : IEncryptor
    {
        public static Type EncryptorType => typeof(RsaEncryptor);

        public required RsaKeyPair KeyPair { get; init; }
        public IEncoder Encoder { get; init; } = Base64Encoder.DefaultInstance;
        public RsaPadding Padding { get; init; } = RsaPadding.Oaep256;

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

        public RsaEncryptor()
        {
        }

        public byte[] Encrypt(byte[] data)
        {
            return data.EncryptRsa(KeyPair.PublicKey, Padding);
        }

        public string Encrypt(string data)
        {
            return Encoder.Encode(Encrypt(data.ToByteArrayUtf8()));
        }

        public byte[] Decrypt(byte[] data)
        {
            return data.DecryptRsa(KeyPair.PrivateKey, Padding);
        }

        public string Decrypt(string data)
        {
            return Decrypt(Encoder.Decode(data)).ToStringUtf8();
        }

        public static IEncryptor Deserialize(string json, byte[] serializeKey)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            Type encoderType = Type.GetType(jsonNode[nameof(Encoder)]![nameof(IEncoder.Name)]!.GetValue<string>())!;
            IEncoder encoder = (IEncoder)JsonSerializer.Deserialize(jsonNode[nameof(Encoder)], encoderType)!;
            string publickey = jsonNode[nameof(KeyPair)]![nameof(RsaKeyPair.PublicKey)]!.GetValue<string?>()!;
            string privatekey = jsonNode[nameof(KeyPair)]![nameof(RsaKeyPair.PrivateKey)]!.GetValue<string?>()!;
            RsaKeyPair keyPair = new RsaKeyPair
            {
                PublicKey = string.IsNullOrWhiteSpace(publickey) ? publickey! : encoder.Decode(publickey).DecryptAesCbc(serializeKey).ToStringUtf8(),
                PrivateKey = string.IsNullOrWhiteSpace(privatekey) ? privatekey! : encoder.Decode(privatekey).DecryptAesCbc(serializeKey).ToStringUtf8()
            };
            return new RsaEncryptor {
                KeyPair = keyPair,
                Encoder = encoder,
                Padding = Enum.Parse<RsaPadding>(jsonNode[nameof(KeyPair)]!.GetValue<uint>().ToString())
            };
        }

        public static IEncryptor Deserialize(string json, string serializeKey)
        {
            throw new NotImplementedException();
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
                [nameof(Encoder)] = Encoder.ToJsonObject(),
                [nameof(KeyPair)] = JsonSerializer.Serialize(new RsaKeyPair
                {
                    PublicKey = string.IsNullOrWhiteSpace(KeyPair.PublicKey) ? KeyPair.PublicKey! :  KeyPair.PublicKey.EncryptAesCbc(serializeKey.ToStringUtf8(), Encoder),
                    PrivateKey = string.IsNullOrWhiteSpace(KeyPair.PrivateKey) ? KeyPair.PrivateKey! : KeyPair.PrivateKey.EncryptAesCbc(serializeKey.ToStringUtf8(), Encoder)
                })
            };
        }

        public JsonObject ToJsonObject(string serializeKey)
        {
            return ToJsonObject(serializeKey.ToByteArrayUtf8());
        }
    }
}
