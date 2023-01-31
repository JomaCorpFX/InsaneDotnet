using Microsoft.Extensions.Configuration;

namespace InsaneIO.Insane.AspNet.Authentication.JwtBearer
{
    public interface IJwtTokenManager
    {
        public JwtToken CreateJwtToken(IConfiguration configuration, string jti, string sub);
        public JwtToken CreateJwtToken(string sub);
    }
}
