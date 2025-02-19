using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NlwApi.Domain;

namespace NlwApi.Infraesructure.Security.Tokens.Access;

public class JwtTokenGenerator
{
  public JwtTokenGenerator()
  {
    DotNetEnv.Env.Load();
  }

  public string GenerateToken(User user)
  {
    var claims = new List<Claim>()
    {
      new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    };
    
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddHours(2),
      Subject = new ClaimsIdentity(claims),
      SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
    
    return tokenHandler.WriteToken(securityToken);
  }

  private static SymmetricSecurityKey SecurityKey()
  {
    var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
    var keyBytes = Encoding.ASCII.GetBytes(jwtKey);
    return new SymmetricSecurityKey(keyBytes);
  }
}