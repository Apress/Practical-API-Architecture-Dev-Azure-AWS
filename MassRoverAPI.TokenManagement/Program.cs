using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MassRoverAPI.TokenManagement
{
    /// <summary>
    /// complete namespaces are used in the code in order remove the confusion between the assemblies 
    /// System.IdentityModel and Microsoft.IdentityModel
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }

        private async Task<System.IdentityModel.Tokens.Jwt.JwtSecurityToken>ValidateAADIdTokenAsync(string idToken)
        {
            var stsDiscoveryEndpoint = "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration"; 

            var configRetriever = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfigurationRetriever();

            var configManager = new Microsoft.IdentityModel.Protocols
                .ConfigurationManager<Microsoft.IdentityModel.Protocols.OpenIdConnect
                .OpenIdConnectConfiguration>(stsDiscoveryEndpoint, configRetriever);

            var config = await configManager.GetConfigurationAsync();

            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                IssuerSigningKeys = config.SigningKeys,
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(idToken, tokenValidationParameters, out var validatedToken);

            return validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
        }

        private string IssueJwtToken(System.IdentityModel.Tokens.Jwt.JwtSecurityToken aadToken)
        {
            var msKey = GetTokenSignKey();

            var msSigningCredentials = new Microsoft.IdentityModel.Tokens
                .SigningCredentials(msKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new System.Security.Claims.ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "thuru@massrover.com"),
                new Claim(ClaimTypes.Role, "admin"),
            }, "MassRover.Authentication");

            var msSecurityTokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Audience = "massrover.client",
                Issuer = "massrover.authservice",
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = msSigningCredentials
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var plainToken = tokenHandler.CreateToken(msSecurityTokenDescriptor);

            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return signedAndEncodedToken;
        }

        private  bool ValidateMassRoverToken(string token)
        {
            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidAudiences = new string[]
                {
                    "massrover.client",
                },

                ValidIssuers = new string[]
                {
                    "massrover.authservice",
                },

                ValidateLifetime = true,

                IssuerSigningKey = GetTokenSignKey()
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            return true;
        }


        private Microsoft.IdentityModel.Tokens.SymmetricSecurityKey GetTokenSignKey()
        {
            var plainTextSecurityKey = "massrover secret key";

            var msKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF32.GetBytes(plainTextSecurityKey));

            return msKey;
        }
    }
}
