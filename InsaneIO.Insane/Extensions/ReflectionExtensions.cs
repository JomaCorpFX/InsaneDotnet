using System.Reflection;

namespace InsaneIO.Insane.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetGenericTypeName(this Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            string genericTypeName = t.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0,
                genericTypeName.IndexOf('`'));
            string genericArgs = string.Join(",",
                t.GetGenericArguments()
                    .Select(ta => GetGenericTypeName(ta)).ToArray());
            return genericTypeName + "<" + genericArgs + ">";
        }

        public static string GetPrincipalName(this Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            string genericTypeName = t.GetGenericTypeDefinition().Name;
            return genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

        }

        public static T ConvertTo<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static MethodInfo GetGenericMethod(this Type type, string name, Func<MethodInfo, bool>? filter, params Type[] args)
        {
            var methods = type.GetMethods().Where(m => m.Name.Equals(name));
            if (filter != null)
            {
                methods = methods.Where(filter);
            }
            return methods.First().MakeGenericMethod(args)!;
        }


    }
}
