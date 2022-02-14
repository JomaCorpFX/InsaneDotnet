using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{
    public class IdentityPostgreSqlDbContext : IdentityCommonDbContextBase<IdentityPostgreSqlDbContext>, IPostgreSqlDbContext
    {
        public IdentityPostgreSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
