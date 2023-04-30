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
            request.Email,
            "New User Registration",
            @"<!DOCTYPE html>
        <html>
            <head>
                <meta charset='utf-8'>
                <title>OTP Verification Code</title>
                <style type='text/css'>
                    body {
                        font-family: Arial, Helvetica, sans-serif;
                    }
                    h1, h2 {
                        color: #333333;
                        text-align: center;
                    }
                    h1 {
                        font-size: 32px;
                    }
                    h2 {
                        font-size: 48px;
                    }
                    p {
                        color: #666666;
                        font-size: 16px;
                        line-height: 1.5;
                        margin: 20px 0;
                        text-align: center;
                    }
                </style>
            </head>
            <body>
                <h1>OTP Verification Code</h1>
                <p>Dear " + newUser.FirstName + @",</p>
                <p>Your OTP verification code is:</p>
                    <h2 style=""background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;"">324457</h2>

                <p>Please enter this code to complete your verification process.</p>
            </body>
        </html>"
        );

        await this.emailService.SendEmailAsync(mailRequest, cancellationToken);


        return newUser.Id;
    }
}