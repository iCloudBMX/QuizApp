using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers;
using QuizApp.Application.Helpers.PasswordHashers;
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
    private readonly IPasswordHasher hasher;



    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IPasswordHasher hasher)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.emailService = emailService;
        this.hasher = hasher;
    }

    public async Task<Result<Guid>> Handle(
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

        var newOtpCode = new OtpCode(
            OtpCodeHelper.GenerateOtpCode());

        newUser.OtpCodes.Add(newOtpCode);
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        var mailRequest = new MailRequest(
           ToEmail: request.Email,
            Subject: EmailMessageExample.GetEmailSubject(),
            Body: EmailMessageExample.GetEmailBody(request.FirstName, newOtpCode.Code));
        try
        {
            await this.emailService.SendEmailAsync(mailRequest, cancellationToken);
        }
        catch (Exception)
        {
            this.userRepository.Delete(newUser);
            await this.unitOfWork.SaveChangesAsync(cancellationToken);
            throw;
        }

        return newUser.Id;
    }
}
