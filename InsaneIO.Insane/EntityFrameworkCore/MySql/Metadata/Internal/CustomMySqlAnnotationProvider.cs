using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;

namespace InsaneIO.Insane.EntityFrameworkCore.MySql.Metadata.Internal
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class CustomMySqlAnnotationProvider : MySqlAnnotationProvider
    {
        public const string AutoincrementAnnotation = "Insane:AutoIncrement";

        public CustomMySqlAnnotationProvider(
            RelationalAnnotationProviderDependencies dependencies,
            IMySqlOptions options)
            : base(dependencies, options)
        {
        }

        public override IEnumerable<IAnnotation> For(ITable table, bool designTime)
        {
            var annotations = base.For(table, designTime);
            IEntityType entityType = table.EntityTypeMappings.First().EntityType;

            IAnnotation? autoIncrement = entityType.FindAnnotation(AutoincrementAnnotation);
            if (autoIncrement is not null)
            {
                annotations = annotations.Append(autoIncrement);
            }
            return annotations;
        }


        //public override IEnumerable<IAnnotation> For(ITable table)
        //{
        //    
        //}

    }
}
