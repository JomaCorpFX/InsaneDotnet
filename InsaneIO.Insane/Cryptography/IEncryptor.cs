using InsaneIO.Insane.Cryptography;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface IEncryptor: IEncryptorJsonSerialize
    {
        public static abstract Type EncryptorType { get; }
        public byte[] Encrypt(byte[] data);
        public string Encrypt(string data);
        public byte[] Decrypt(byte[] data);
        public string Decrypt(string data);
    }
}
