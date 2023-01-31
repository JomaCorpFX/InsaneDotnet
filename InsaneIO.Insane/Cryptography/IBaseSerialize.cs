using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Cryptography
{
    public interface IBaseSerialize
    {
        public string Name { get; init; }

        public static string GetName(Type implementationType)
        {
            return $"{implementationType.FullName!}, {implementationType.Assembly.GetName().Name}";
        }
    }
}
