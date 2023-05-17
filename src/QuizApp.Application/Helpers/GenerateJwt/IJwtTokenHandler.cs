using QuizApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
namespace QuizApp.Application.Helpers.Generatewt;

public interface IJwtTokenHandler
{
    string GenerateAccessToken(User user, string token);
    string GenerateRefreshToken();
}

