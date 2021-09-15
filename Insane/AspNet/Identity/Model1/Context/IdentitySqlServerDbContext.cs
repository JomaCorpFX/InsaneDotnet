using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class IdentitySqlServerDbContext : IdentityDbContextBase, ISqlServerDbContext
    {
        public IdentitySqlServerDbContext(DbContextOptions<IdentitySqlServerDbContext> options) : base(options)
        {
        }

    }
}
