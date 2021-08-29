using Insane.Converter;
using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public interface IEntityProtector<TEntity>
        where TEntity : class
    {
        public TEntity Protect(TEntity entity);
        public TEntity Unprotect(TEntity entity);
    }
    
    
}
