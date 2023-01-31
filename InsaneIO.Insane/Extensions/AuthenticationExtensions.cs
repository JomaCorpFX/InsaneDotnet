using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace InsaneIO.Insane.Extensions
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services, Action<JwtBearerOptions> configureOptions)
        {
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(configureOptions);
        }
    }
}
