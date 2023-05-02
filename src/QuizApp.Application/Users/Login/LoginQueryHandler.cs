using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers.Generatewt;
using QuizApp.Application.Helpers.PasswordHashers;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.IdentityModel.Tokens.Jwt;

namespace QuizApp.Application.Users.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, LoginQueryResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IJwtTokenHandler jwtTokenHandler;

        public LoginQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenHandler jwtTokenHandler)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.jwtTokenHandler = jwtTokenHandler;
        }

        public async Task<Result<LoginQueryResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            string loginEmail = request.Email;
            string loginPassword = request.Password;
            var maybeUser = await this.userRepository.SelectUserWithEmailAsync(loginEmail);
            if (maybeUser is null)
            {
                return Result.Failure<LoginQueryResponse>(
                    Domain.Errors.DomainErrors.User.UserNotFoundByCredentials(loginEmail, loginPassword));
            }

            bool checkPassword = this.passwordHasher.CheckPassword(
                hashedPassword: maybeUser.PasswordHash,
                password: loginPassword,
                salt: maybeUser.Salt);

            if (checkPassword is false)
            {
                return Result.Failure<LoginQueryResponse>(
                     Domain.Errors.DomainErrors.User.UserNotFoundByCredentials(loginEmail, loginPassword));
            }

            JwtSecurityToken accessToken = this.jwtTokenHandler
                .GenerateAccessToken(maybeUser);

            string token = new JwtSecurityTokenHandler()
                .WriteToken(accessToken);

            var loginResponse = new LoginQueryResponse(
                token, accessToken.ValidTo);

            return loginResponse;
        }
    }
}
