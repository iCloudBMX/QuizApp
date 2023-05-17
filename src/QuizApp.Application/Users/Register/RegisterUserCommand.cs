using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Users.Register;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    string Password) : ICommand<RegisterUserResponse>;