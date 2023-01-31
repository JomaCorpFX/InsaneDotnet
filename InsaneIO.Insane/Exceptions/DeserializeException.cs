namespace InsaneIO.Insane.Exceptions
{
    public class DeserializeException : SystemException
    {
        private readonly Type type;
        private readonly string content;

        public DeserializeException(Type type, string content = "")
        {
            this.type = type;
            this.content = content;
        }

        public override string Message => $"Invalid content \"{content}\" to deserialize for the type \"{type.Name}\".";
    }
}
