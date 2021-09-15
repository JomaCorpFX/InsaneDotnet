using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.EntityFrameworkCore
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public readonly DatabaseFacade Database;
        public readonly string Schema;
        public EntityTypeConfigurationBase(DatabaseFacade database, string schema)
        {
            Database = database;
            Schema = schema;
        }

        public EntityTypeConfigurationBase(DatabaseFacade database)
        {
            Database = database;
            Schema = null!;
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
