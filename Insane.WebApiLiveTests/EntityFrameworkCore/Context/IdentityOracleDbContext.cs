using Insane.EntityFrameworkCore;
using Insane.WebApiLiveTests.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{
    public class IdentityOracleDbContext : IdentityCommonDbContextBase<IdentityOracleDbContext>, IOracleDbContext
    {
        public IdentityOracleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
