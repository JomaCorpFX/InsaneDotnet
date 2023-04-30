using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface IEncoder : IEncoderJsonSerialize
    {
        public static abstract Type EncoderType { get; } 
        public string Encode(byte[] data);
        public byte[] Decode(string data);
    }
}
