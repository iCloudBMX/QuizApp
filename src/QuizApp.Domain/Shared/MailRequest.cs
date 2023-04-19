namespace QuizApp.Domain.Shared;

public record MailRequest(
    string ToEmail,
    string Subject,
    string Body);
