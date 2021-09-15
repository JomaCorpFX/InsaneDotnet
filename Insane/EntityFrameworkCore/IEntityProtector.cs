using Insane.Converter;
using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
{
    public interface IEntityProtector<TEntity>
        where TEntity : class, IEntity
    {
        public TEntity Protect(TEntity entity);
        public TEntity Unprotect(TEntity entity);
    }
    
    
}
