using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.EntityFramework
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public readonly DatabaseFacade Database;
        public EntityTypeConfigurationBase(DatabaseFacade database)
        {
            Database = database;
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
