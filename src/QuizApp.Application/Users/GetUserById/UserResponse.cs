namespace QuizApp.Application.Users.GetUserById;

public record UserResponse(
    Guid Id,
    string Name,
    string LastName,
    string Phone,
    string Email,
    int Role,
    DateTime RegisteredAt,
    int Status);