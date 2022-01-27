using Insane.EntityFrameworkCore.MySql.Metadata.Internal;
using Insane.EntityFrameworkCore.MySql.Migrations.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
{
    public abstract class CoreDbContextBase : DbContext
    {
        public string? Schema { get; init; }

        private CoreDbContextBase()
        {

        }

        public CoreDbContextBase(DbContextOptions options, string? schema) : base(options)
        {
            Schema = schema;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.Options.ContextType.GetInterfaces().Contains(typeof(IMySqlDbContext)))
            {
                ServiceProvider serviceProvider = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .AddSingleton<IRelationalAnnotationProvider, CustomMySqlAnnotationProvider>()
                .AddScoped<IMigrationsSqlGenerator, CustomMySqlMigrationsSqlGenerator>()
                .BuildServiceProvider();
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            }
            base.OnConfiguring(optionsBuilder);
        }


    }
}
