namespace InsaneIO.Insane.AspNet.Authentication.JwtBearer
{
    public record JwtToken(string Token, string Jti, DateTimeOffset ExpirationTime);
}
