namespace QuizApp.Application.Users.GetUserById;

public record UserResponse(
    Guid Id,
    string Firstname,
    string LastName,
    string Phone,
    string Email,
    int Role,
    DateTime RegisteredAt,
    int Status);