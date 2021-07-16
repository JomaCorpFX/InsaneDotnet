using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class ReflectionExtensions
    {
        public static T ConvertTo<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static MethodInfo GetGenericMethod(this Type type, string name, Func<MethodInfo, bool>? filter, params Type [] args)
        {
            var methods = type.GetMethods().Where(m => m.Name.Equals(name));
            if(filter != null)
            {
                methods = methods.Where(filter);
            }
            return methods.First().MakeGenericMethod(args)!;
        }
    }
}
