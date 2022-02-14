using Insane.AspNet.Identity.Model1;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{
    public class IdentityMySqlDbContext : IdentityCommonDbContextBase<IdentityMySqlDbContext>, IMySqlDbContext
    {
        
        public IdentityMySqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
