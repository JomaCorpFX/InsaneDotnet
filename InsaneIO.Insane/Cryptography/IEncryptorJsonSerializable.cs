using InsaneIO.Insane.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    
    public interface IEncryptorJsonSerializable: IJsonSerializable
    {
        public static abstract IEncryptor Deserialize(string json);
    }
}
