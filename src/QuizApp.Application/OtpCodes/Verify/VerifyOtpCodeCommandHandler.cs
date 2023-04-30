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

    public VerifyOtpCodeCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IOtpCodeRepository otpCodeRepository)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.otpCodeRepository = otpCodeRepository;
    }

    public async Task<Result<Guid>> Handle(
        VerifyOtpCodeCommand request,
        CancellationToken cancellationToken)
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
}
