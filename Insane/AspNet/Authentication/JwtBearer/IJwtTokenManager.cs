using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Authentication.JwtBearer
{
    public interface IJwtTokenManager
    {
        public JwtToken CreateJwtToken(IConfiguration configuration, string jti, string sub);
        public JwtToken CreateJwtToken(string sub);
    }
}
