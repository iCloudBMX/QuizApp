﻿using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Interfaces;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailService emailService;
    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IValidator<RegisterUserCommand> validator)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.emailService = emailService;
        this.validator = validator;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var valideResult = validator.Validate(request);

        if (valideResult.IsValid)
        {
            string randomSalt = Guid.NewGuid().ToString();

            string passwordHash =
                PasswordHasher.CreatePasswordHash(
                    password: request.Password,
                    randomSalt);

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
                $"Your verification code is {newOtpCode.Code}");

            await this.emailService.SendEmailAsync(mailRequest, cancellationToken);

            return newUser.Id;
        }
        else
        {
            StringBuilder validateErrors = new StringBuilder();
            foreach (var item in valideResult.Errors)
            {
                validateErrors.AppendLine(item.ErrorMessage);
            }

            var errors = new Error("Validation error", validateErrors.ToString());

            return Result.Failure<Guid>(errors);
        }
    }
}