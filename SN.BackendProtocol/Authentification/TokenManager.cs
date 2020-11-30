using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using SN.GlobalAbstractions.Logging;

namespace SN.BackendProtocol.Authentification
{
    public class TokenManager
    {

        private readonly ILoggingService loggingService;

        private readonly string secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public TokenManager(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public string GenerateToken(string username, string persistentId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var issuer = "http://getsinalia.com";
            var audience = "http://getsinalia.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, username.ToString()),
                    new Claim(JwtClaimTypes.Subject, persistentId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var issuer = "http://getsinalia.com";
            var audience = "http://getsinalia.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
