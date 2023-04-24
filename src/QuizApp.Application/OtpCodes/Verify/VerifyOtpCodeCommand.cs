using QuizApp.Application.Abstractions;

namespace QuizApp.Application;

public record VerifyOtpCodeCommand(string OtpCode, Guid UserId) : ICommand<Guid>;
