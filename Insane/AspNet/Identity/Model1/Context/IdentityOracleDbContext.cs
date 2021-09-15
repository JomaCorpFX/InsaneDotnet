using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class IdentityOracleDbContext : IdentityDbContextBase, IOracleDbContext
    {
        public IdentityOracleDbContext(DbContextOptions<IdentityOracleDbContext> options) : base(options)
        {
        }
    }
}
