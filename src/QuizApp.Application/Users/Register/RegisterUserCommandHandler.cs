using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers.Generatewt;
using QuizApp.Application.Helpers.PasswordHashers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.Register;

internal sealed class RegisterUserCommandHandler
    : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IJwtTokenHandler jwtTokenHandler;
    private readonly IPasswordHasher hasher;
    private readonly IUserSessionRepository userSessionRepository;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHasher hasher,
        IUserSessionRepository userSessionRepository)
    {
        this.userRepository=userRepository;
        this.unitOfWork=unitOfWork;
        this.jwtTokenHandler=jwtTokenHandler;
        this.hasher=hasher;
        this.userSessionRepository=userSessionRepository;
    }

    public async Task<Result<RegisterUserResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        string randomSalt = Guid.NewGuid().ToString();

        string passwordHash = hasher.CreatePasswordHash(request.Password, randomSalt);

        var newUser = new User(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email,
            randomSalt,
            passwordHash,
            UserRole.Tester);

        this.userRepository.Insert(newUser);

        var session = new UserSession(Guid.NewGuid().ToString(), newUser.Id);

        userSessionRepository.Insert(session);
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        var token = jwtTokenHandler.GenerateAccessToken(newUser, session.Token);

        var response = new RegisterUserResponse(token);

        return response;
    }
}
