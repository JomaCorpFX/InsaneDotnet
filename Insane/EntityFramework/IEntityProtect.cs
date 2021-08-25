using Insane.Converter;
using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public interface IEntityProtect<TEntity>
        where TEntity : class
    {
        public TEntity Protect();
        public TEntity Unprotect();
    }

    
}
