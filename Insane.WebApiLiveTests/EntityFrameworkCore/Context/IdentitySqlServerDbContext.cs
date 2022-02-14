using Insane.Core;
using Insane.EntityFrameworkCore;
using Insane.WebApiLiveTests.EntityFrameworkCore.Factory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{
    public class IdentitySqlServerDbContext : IdentityCommonDbContextBase<IdentitySqlServerDbContext>, ISqlServerDbContext
    {
        public IdentitySqlServerDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
