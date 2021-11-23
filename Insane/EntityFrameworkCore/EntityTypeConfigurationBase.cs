using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.EntityFrameworkCore
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class,IEntity
    {
        public readonly DatabaseFacade Database;
        public readonly string? Schema;
        public EntityTypeConfigurationBase(DatabaseFacade database, string? schema= null)
        {
            Database = database;
            Schema = schema;
        }


        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
