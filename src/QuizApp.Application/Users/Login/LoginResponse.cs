namespace QuizApp.Application.Users.Login;

public record class LoginQueryResponse(
    string AccessToken,
    string RefreshToken);
