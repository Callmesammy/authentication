

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthXY.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthXy.Service;

public class TokenService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string CreateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };
        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
    );
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims,
        expires: DateTime.UtcNow.AddDays(7),
        signingCredentials: creds,
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt: Audience"]);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}