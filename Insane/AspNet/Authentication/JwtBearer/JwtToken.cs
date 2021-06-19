using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Authentication.JwtBearer
{
    public record JwtToken(string Token, string Jti, DateTimeOffset ExpirationTime);
}
