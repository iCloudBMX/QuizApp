using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.OtpCodes;

public class SendOtpCommandHandler : ICommandHandler<SendOtpCommand, bool>
{
    private readonly IUserRepository userRepository;
    private readonly IEmailService emailService;
    private readonly IUnitOfWork unitOfWork;

    public SendOtpCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        this.userRepository=userRepository;
        this.emailService=emailService;
        this.unitOfWork=unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        var maybeUser = await userRepository
            .SelectUserWithOtpCodesAsync(request.UserId);

        if ( maybeUser==null )
        {
            return Result.Failure<bool>(
                Domain.Errors.DomainErrors.User.NotFound(request.UserId));
        }

        if ( maybeUser.Email!=request.Email )
        {
            return Result.Failure<bool>(
                Domain.Errors.DomainErrors.User.NotFound(request.UserId));
        }

        var lastOtp = maybeUser.OtpCodes
            .OrderByDescending(otp => otp.CreatedAt)
            .FirstOrDefault();

        if ( lastOtp is not null&&!lastOtp.IsExpired() )
        {
            return Result.Success<bool>(true);
        }

        var newOtpCode = new OtpCode(
            OtpCodeHelper.GenerateOtpCode());

        maybeUser.OtpCodes.Add(newOtpCode);


        var mailRequest = new MailRequest(
           ToEmail: request.Email,
            Subject: EmailMessageExample.GetEmailSubject(),
            Body: EmailMessageExample.GetEmailBody(maybeUser.FirstName, newOtpCode.Code));

        await this.emailService.SendEmailAsync(mailRequest, cancellationToken);

        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success<bool>(true);
    }
}
