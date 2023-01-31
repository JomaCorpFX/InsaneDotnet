using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    [RequiresPreviewFeatures]
    public interface IHasherJsonSerialize : IJsonSerialize
    {
        public static abstract IHasher Deserialize(string json);
    }
}
