using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Serialization
{
    [RequiresPreviewFeatures]
    public interface IBaseSerialize
    {
        public static abstract Type SelfType { get; }

        public string Name { get; }

        public static string GetName(Type implementationType)
        {
            return $"{implementationType.FullName!}, {implementationType.Assembly.GetName().Name}";
        }
    }
}
