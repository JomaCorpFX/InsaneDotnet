using Microsoft.Extensions.Configuration;

namespace InsaneIO.Insane.AspNet.Authentication.JwtBearer
{
    public class JwtTokenManager : IJwtTokenManager
    {
        public JwtTokenManager(IConfiguration configuration)
        {

        }

        public JwtToken CreateJwtToken(IConfiguration configuration, string jti, string sub)
        {
            throw new NotImplementedException();
        }

        public JwtToken CreateJwtToken(string sub)
        {
            throw new NotImplementedException();
        }
    }
}
