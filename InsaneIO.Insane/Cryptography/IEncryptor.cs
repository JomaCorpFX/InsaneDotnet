using InsaneIO.Insane.Cryptography;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface IEncryptor: IEncryptorJsonSerialize
    {
        public byte[] Encrypt(byte[] data);
        public string EncryptEncoded(string data);
        public byte[] Decrypt(byte[] data);
        public string DecryptEncoded(string data);
    }
}
