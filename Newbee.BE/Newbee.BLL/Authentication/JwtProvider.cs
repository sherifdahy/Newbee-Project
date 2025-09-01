using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newbee.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Newbee.BLL.Authentication;

public class JwtProvider(IOptions<JwtOptions>options) : IJwtProvider
{
    private readonly JwtOptions options = options.Value;

    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email,user.Email!),
          new(JwtRegisteredClaimNames.GivenName,user.FirstName!),
            new(JwtRegisteredClaimNames.FamilyName,user.LastName!),
  new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            ];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var expirationDate= DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);
        var token = new JwtSecurityToken(
            issuer:options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: signingCredentials
            );
       return(token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: options.ExpiryMinutes*60);
    }
}
