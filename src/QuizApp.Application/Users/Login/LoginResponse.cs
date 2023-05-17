using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Users.Login
{
   public record class LoginResponse(
        string AccessToken,
        DateTime expireDate);
}
