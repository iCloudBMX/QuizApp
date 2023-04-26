using System.Security.Cryptography;
using System.Text;

namespace QuizApp.Application.Helpers;

internal static class PasswordHasher 
{
    private const int KeySize = 32;
    private const int IterationCount = 1000;

    public static string CreatePasswordHash(string password, string salt)
    {
        using (var algoritm = new Rfc2898DeriveBytes(
        password: password,
        salt: Encoding.UTF8.GetBytes(salt),
        iterations: IterationCount,
        hashAlgorithm: HashAlgorithmName.SHA256))

        {
            return Convert.ToBase64String(algoritm.GetBytes(KeySize));
        }
    }
    public static bool CheckPassword(string hashedPassword, string password, string salt) =>
        CreatePasswordHash(password, salt).SequenceEqual(hashedPassword);
}
