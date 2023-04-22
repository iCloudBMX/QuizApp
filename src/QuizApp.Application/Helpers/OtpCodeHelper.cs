namespace QuizApp.Application.Helpers;

public static class OtpCodeHelper
{
    public static string GenerateOtpCode(int length = 6)
    {
        char[] randomCharacterList = new char[length];
        string characters = "0123456789";

        for(int i = 0; i < length; i++)
        {
            int randomCharacterIndex = Random.Shared.Next(0, characters.Length);
            randomCharacterList[i] = characters[randomCharacterIndex];
        }

        return new string(randomCharacterList);
    }
}
