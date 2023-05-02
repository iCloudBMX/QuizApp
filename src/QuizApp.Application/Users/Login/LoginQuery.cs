using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Users.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IQuery<LoginQueryResponse>;
}
