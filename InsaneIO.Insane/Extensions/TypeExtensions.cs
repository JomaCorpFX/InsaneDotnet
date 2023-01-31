namespace InsaneIO.Insane.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsIntOrLongType(this Type type)
        {
            return typeof(int).Equals(type) || typeof(long).Equals(type);
        }

        public static bool IsIntType(this Type type)
        {
            return typeof(int).Equals(type);
        }

        public static bool IsLongType(this Type type)
        {
            return typeof(long).Equals(type);
        }

        public static bool IsStringType(this Type type)
        {
            return typeof(string).Equals(type);
        }
    }
}
