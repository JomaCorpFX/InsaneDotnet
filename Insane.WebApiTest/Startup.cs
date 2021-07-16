using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Insane.EntityFramework;
using Insane.AspNet.Identity.Model1.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Insane.WebApiTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false, true)
                   .AddUserSecrets<Startup>()
                   .Build();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder().EnableSensitiveDataLogging().EnableDetailedErrors();
            DbContextFlavors<IdentityDbContextBase> flavors = DbContextFlavors<IdentityDbContextBase>.CreateInstance<IdentitySqlServerDbContext,
                IdentityPostgreSqlDbContext,
                IdentityMySqlDbContext,
                IdentityOracleDbContext>();
            services.AddDbContext<IdentityDbContextBase>(builder, Configuration, nameof(DbContextSettings), flavors, ServiceLifetime.Scoped);
            services.AddIdentity<IdentityUser, IdentityRole>(op=> {
                op.Password = new PasswordOptions
                {
                    
                };
                op.Lockout = new LockoutOptions
                {
                    
                };
                op.SignIn = new SignInOptions
                {
                    
                };
                op.Stores = new StoreOptions
                {
                    
                };

                op.Tokens = new TokenOptions
                {
                    
                };

                op.ClaimsIdentity = new ClaimsIdentityOptions
                {
                    EmailClaimType = System.Security.Claims.ClaimTypes.WindowsAccountName
                };

            }).AddEntityFrameworkStores<IdentityDbContext>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Insane.WebApiTest", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityDbContextBase context)
        {

            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insane.WebApiTest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
