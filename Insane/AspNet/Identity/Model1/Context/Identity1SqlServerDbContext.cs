using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class Identity1SqlServerDbContext : Identity1DbContextBase, ISqlServerDbContext
    {
        public Identity1SqlServerDbContext(DbContextOptions options) : base(options, IdentityConstants.DefaultSchema)
        {
        }
    }
}
