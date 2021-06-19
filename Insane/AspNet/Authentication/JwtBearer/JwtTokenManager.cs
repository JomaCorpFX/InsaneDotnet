using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Authentication.JwtBearer
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
