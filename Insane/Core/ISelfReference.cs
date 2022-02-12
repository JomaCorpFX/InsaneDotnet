using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Core
{
    public interface ISelfReference<T>
    {
        public static Type GetContainedType()
        {
            return typeof(T);
        }
    }
}
