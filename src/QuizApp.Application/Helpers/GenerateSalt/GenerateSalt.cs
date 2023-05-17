using System.Text;

namespace QuizApp.Application.Helpers.GenerateSalt;

public class GenerateSalt : IGenerateSalt
{
    static string charList = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
    const int stringLength = 64;
    public string GenerateStrng()
    {
        Random random = new Random();
        StringBuilder randomSalt = new StringBuilder();

        for (int i = 0; i < stringLength; i++)
        {
            randomSalt
                .Append(charList[random.Next(charList.Length)]);
        }

        return randomSalt.ToString();
    }
}
