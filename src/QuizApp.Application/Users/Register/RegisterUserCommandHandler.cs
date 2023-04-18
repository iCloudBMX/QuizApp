using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IOtpCodeRepository otpCodeRepository;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IOtpCodeRepository otpCodeRepository)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.otpCodeRepository = otpCodeRepository;
    }

    public Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var newUser = new User(
            )
    }
}