namespace InsaneIO.Insane.EntityFrameworkCore
{

    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CoreDbContextFactoryAttribute : Attribute
    {
        public string Name { get; }

        public CoreDbContextFactoryAttribute(string Name)
        {
            this.Name = Name;
        }

    }
}
