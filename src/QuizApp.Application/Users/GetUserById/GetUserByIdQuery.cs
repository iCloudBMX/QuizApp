using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Users.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
