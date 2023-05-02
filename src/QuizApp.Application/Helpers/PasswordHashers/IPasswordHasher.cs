namespace QuizApp.Application.Helpers.PasswordHashers;

public interface IPasswordHasher
{
    string CreatePasswordHash(string password, string salt);
    bool CheckPassword(string hashedPassword, string password, string salt);
}
