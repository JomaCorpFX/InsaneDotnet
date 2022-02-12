using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class IdentityPostgreSqlDbContext : IdentityDbContextString<IdentityPostgreSqlDbContext>, IPostgreSqlDbContext
    {
        public IdentityPostgreSqlDbContext(DbContextOptions options, string? defaultSchema = null) : base(options, defaultSchema)
        {
        }
    }
}
