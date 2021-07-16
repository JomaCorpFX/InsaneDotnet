﻿using Insane.EntityFramework.MySql.Metadata.Internal;
using Insane.EntityFramework.MySql.Migrations.Internal;
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

namespace Insane.EntityFramework
{
    public abstract class CoreDbContextBase : DbContext
    {

        private CoreDbContextBase()
        {

        }

        public CoreDbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.Options.ContextType.GetInterfaces().Contains(typeof(IMySqlDbContext)))
            {
                ServiceProvider serviceProvider = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .AddSingleton<IRelationalAnnotationProvider, CustomMySqlAnnotationProvider>()
                .AddScoped<IMigrationsSqlGenerator, CustomMySqlMigrationsSqlGenerator>()
                .BuildServiceProvider();
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            }

        }


    }
}
