using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application;

internal class VerifyOtpCodeCommandHandler : ICommandHandler<VerifyOtpCodeCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IOtpCodeRepository otpCodeRepository;
    private readonly IValidator<VerifyOtpCodeCommand> validator;

    public VerifyOtpCodeCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IOtpCodeRepository otpCodeRepository,
        IValidator<VerifyOtpCodeCommand> validator)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.otpCodeRepository = otpCodeRepository;
        this.validator = validator;
    }

    public async Task<Result<Guid>> Handle(
        VerifyOtpCodeCommand request,
        CancellationToken cancellationToken)
    {
        var validateResult=validator.Validate(request);
        if(validateResult.IsValid)
        {

        User maybeUser = await this.userRepository
            .SelectUserWithOtpCodesAsync(request.UserId);

        if(maybeUser is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.User.NotFound(request.UserId));
        }

        OtpCode? lastOtpCode = maybeUser.OtpCodes
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault();

        if(lastOtpCode is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.OtpCode.InValid());
        }

        if(lastOtpCode.IsExpired())
        {
            lastOtpCode.MarkAsExpired();
            this.otpCodeRepository.Update(lastOtpCode);
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Failure<Guid>(
                DomainErrors.OtpCode.Expired());
        }

        if(!lastOtpCode.IsValid(request.OtpCode))
        {
            return Result.Failure<Guid>(
                DomainErrors.OtpCode.InValid());
        }

        lastOtpCode.MarkAsVerified();
        this.otpCodeRepository.Update(lastOtpCode);

        maybeUser.MarkAsActive();
        this.userRepository.Update(maybeUser);

        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return maybeUser.Id;
        }
        else
        {
            StringBuilder validateErrors = new StringBuilder();
            foreach (var item in validateResult.Errors)
            {
                validateErrors.AppendLine(item.ErrorMessage);
            }
            var errors = new Error("Validation error", validateErrors.ToString());

            return Result.Failure<Guid>(errors);
        }
    }
}
