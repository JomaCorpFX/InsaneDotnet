namespace InsaneIO.Insane.EntityFrameworkCore
{
    public interface IEntityProtector<TEntity>
        where TEntity : class, IEntity
    {
        public TEntity Protect(TEntity entity);
        public TEntity Unprotect(TEntity entity);
    }


}
