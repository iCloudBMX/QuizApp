using QuizApp.Domain.Shared;

namespace QuizApp.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "User.NotFound",
            message: $"The user with the identifier {id} was not found.");
    }
    public static class Question
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "Question.NotFound",
            message: $"The question with the identifier {id} was not found.");
    }
}
