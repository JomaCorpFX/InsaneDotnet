using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework.MySql.Metadata.Internal
{
    

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class CustomMySqlAnnotationProvider : MySqlAnnotationProvider
    {
        public const string AutoincrementAnnotation = "Insane:AutoIncrement";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public CustomMySqlAnnotationProvider(
            RelationalAnnotationProviderDependencies dependencies,
            IMySqlOptions options)
            : base(dependencies, options)
        {
        }


        public override IEnumerable<IAnnotation> For(ITable table)
        {
            var annotations = base.For(table);
            IEntityType entityType = table.EntityTypeMappings.First().EntityType;

            IAnnotation autoIncrement = entityType.FindAnnotation(AutoincrementAnnotation);
            if (autoIncrement is not null)
            {
                annotations = annotations.Append(autoIncrement);
            }

            return annotations;
        }


    }
}
