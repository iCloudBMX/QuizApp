using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace QuizApp.Application.Helpers.Generatewt;

public class JwtTokenHandler : IJwtTokenHandler
{

    private readonly JwtOption jwtOption;

    public JwtTokenHandler(IOptions<JwtOption> jwtOption)
    {
        this.jwtOption = jwtOption.Value;
    }

    public JwtSecurityToken GenerateAccessToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(CustomClaimNames.Id, user.Id.ToString()),
            new Claim(CustomClaimNames.Email, user.Email),
            new Claim(CustomClaimNames.Role, Enum.GetName(user.Role))
        };

        var authSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(this.jwtOption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: this.jwtOption.Issuer,
            audience: this.jwtOption.Audience,
            expires: DateTime.UtcNow.AddMinutes(this.jwtOption.ExpirationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
