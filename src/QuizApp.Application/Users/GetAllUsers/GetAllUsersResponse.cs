using QuizApp.Application.Users.GetUserById;

namespace QuizApp.Application.Users.GetAllUsers
{
    public record GetAllUsersResponse(
        IList<UserResponse>allUsers);
}
