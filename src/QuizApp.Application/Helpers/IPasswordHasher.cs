namespace QuizApp.Application.Helpers
{
    internal interface IPasswordHasher
    {
        string CreatePasswordHash(string password, string salt);
        bool CheckPassword(string hashedPassword, string password, string salt);
    }
}
