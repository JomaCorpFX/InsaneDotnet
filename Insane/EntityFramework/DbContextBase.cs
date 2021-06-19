using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public abstract class DbContextBase : DbContext
    {

        private DbContextBase()
        {
            
        }

        public DbContextBase(DbContextOptions options) : base(options)
        {

        }

       
    }
}
