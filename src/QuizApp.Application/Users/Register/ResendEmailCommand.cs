using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Users.Register
{
    public record ResendEmailCommand(Guid userId) : ICommand<Guid>;
  
}
