using QuizApp.Application.Abstractions;

namespace QuizApp.Application.OtpCodes;

public record SendOtpCommand(
    Guid UserId,
    string Email) : ICommand<bool>;