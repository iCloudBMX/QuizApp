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

    public static class Tag
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            code: "Tag.NotFound",
            message: $"The tag with the identifier {id} was not found.");
    }
}
