using System.Runtime.Versioning;

namespace InsaneIO.Insane.Serialization
{
    [RequiresPreviewFeatures]
    public interface IBaseSerializable
    {
        public static abstract Type SelfType { get; }

        public string AssemblyName { get; }

        public static string GetName(Type implementationType)
        {
            return $"{implementationType.FullName!}, {implementationType.Assembly.GetName().Name}";
        }
    }
}