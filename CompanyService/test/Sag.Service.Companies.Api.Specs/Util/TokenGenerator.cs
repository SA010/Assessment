//TODO: //using Sag.Service.Identity;
//using Sag.Service.Identity.Client;
//using Sag.Service.Identity.Client.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sag.Service.Companies.Api.Specs.Util
{
    /*
    public class TokenGenerator
    {
        private readonly IdentitySettings _settings;
        private readonly JwtSecurityTokenHandler _handler;

        public TokenGenerator(IOptions<IdentitySettings> settings)
        {
            _settings = settings.Value;
            _handler = new JwtSecurityTokenHandler();
        }

        public TokenDto GenerateToken(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = GetClaims(user);

            var token = new JwtSecurityToken(
                _settings.ValidIssuer,
                _settings.ValidAudience,
                claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: MockValidationKeyProvider.GetSigningCredentials()
            );

            return new TokenDto
            {
                Token = _handler.WriteToken(token),
                TokenValidTo = token.ValidTo,
                RefreshToken = Guid.NewGuid(),
                RefreshTokenValidTo = token.ValidTo
            };
        }

        private static IEnumerable<Claim> GetClaims(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaims.UserId, user.Id.ToString()),
            };

            if (user.Roles?.Count > 0)
            {
                var roles = user.Roles.Select(x => x.Role).Distinct();
                claims.AddRange(roles.Select(x => new Claim(CustomClaims.Roles, x)));

                var companyIds = user.Roles.Where(x => x.CompanyId != null).Select(x => x.CompanyId!.Value.ToString()).Distinct();
                claims.AddRange(companyIds.Select(x => new Claim(CustomClaims.CompanyIds, x)));

                var modules = user.Roles.Select(x => x.Module).Distinct();
                claims.AddRange(modules.Select(x => new Claim(CustomClaims.Modules, x)));
            }

            return claims;
        }
    }
    */
}