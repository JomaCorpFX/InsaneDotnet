using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.EntityFrameworkCore
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public DatabaseFacade Database{ get;init;}

        public EntityTypeConfigurationBase(DatabaseFacade database)
        {
            Database = database;
        }


        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
