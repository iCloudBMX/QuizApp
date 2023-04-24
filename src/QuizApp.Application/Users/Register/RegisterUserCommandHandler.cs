using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Interfaces;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailService emailService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.emailService = emailService;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        string passwordHash = request.Password;
        
        var newUser = new User(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email,
            passwordHash,
            UserRole.Tester);

        this.userRepository.Insert(newUser);

        var newOtpCode = new OtpCode(
            OtpCodeHelper.GenerateOtpCode());

        newUser.OtpCodes.Add(newOtpCode);
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        var mailRequest = new MailRequest(
            request.Email,
            "New User Registration",
            $"Your verification code is {newOtpCode.Code}");

        await this.emailService.SendEmailAsync(mailRequest, cancellationToken);

        return newUser.Id;
    }
}