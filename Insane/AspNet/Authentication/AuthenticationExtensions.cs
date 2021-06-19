using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Authentication
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services, Action<JwtBearerOptions> configureOptions)
        {
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(configureOptions);
        }
    }
}
