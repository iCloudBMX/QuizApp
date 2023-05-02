using QuizApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
namespace QuizApp.Application.Helpers.Generatewt;

public interface IJwtTokenHandler
{
    JwtSecurityToken GenerateAccessToken(User user);
}

