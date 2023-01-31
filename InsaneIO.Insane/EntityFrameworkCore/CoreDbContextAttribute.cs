using System.ComponentModel.DataAnnotations;using System.Xml.Serialization;

namespace InsaneIO.Insane.EntityFrameworkCore
{
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CoreDbContextAttribute : Attribute
    { 
        public string Name { get; }
        public CoreDbContextAttribute(string Name)
        {
            this.Name = Name;
        }

    }
}


//[CoreDbContextFactory("ARdpce")]
//public abstract class InsaneZFactory<TContext> : CoreDbContextFactoryBase<TContext>
//    where TContext : CoreDbContextBase
//{
//    //Flavors<RegistroMobileSqlServerDbContext, RegistroMobilePostgreSqlDbContext, RegistroMobileMySqlDbContext, RegistroMobileOracleDbContext> Flavors;
//}

//[CoreDbContextFactory("AInsane")]
//public abstract class Insane3DFactory<TContext> : CoreDbContextFactoryBase<TContext>
//    where TContext : CoreDbContextBase
//{

//}


[CoreDbContext("Example")]
public abstract class ExampleContextBase : CoreDbContextBase
{
    
    public ExampleContextBase([Required] DbContextOptions options, int z) : base(options)
    {
        
        //Some code
        
    }

    public ExampleContextBase(DbContextOptions options, long l) : base(options)
    {
        //Some code
    }
}