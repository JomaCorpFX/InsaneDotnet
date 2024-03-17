using InsaneIO.Insane.Serialization;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;

namespace InsaneIO.Insane.Cryptography
{
    
    public interface IEncoder : IEncoderJsonSerializable
    {
        public string Encode(byte[] data);
        public string Encode(string data);
        public byte[] Decode(string data);
    }
}
