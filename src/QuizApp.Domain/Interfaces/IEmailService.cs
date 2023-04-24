using QuizApp.Domain.Shared;

namespace QuizApp.Domain.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(
        MailRequest mailRequest,
        CancellationToken cancellationToken);
}