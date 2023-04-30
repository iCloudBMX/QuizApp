using QuizApp.Application.Abstractions;
using QuizApp.Application.Helpers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Interfaces;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.Register
{
    public class ResendEmailCommandHandler : ICommandHandler<ResendEmailCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IUnitOfWork unitOfWork;

        public ResendEmailCommandHandler(
            IUserRepository userRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            ResendEmailCommand request,
            CancellationToken cancellationToken)
        {
            var maybeUser =
                await userRepository.SelectUserWithOtpCodesAsync(
                    request.userId);

            if (maybeUser is null)
            {
                return Result.Failure<Guid>(
                    Domain.Errors.DomainErrors.User.NotFound(request.userId));
            }

            OtpCode? lastOtpCode = maybeUser.OtpCodes
                .OrderByDescending(otp => otp.CreatedAt)
                .FirstOrDefault();

            if (lastOtpCode is not null
               && !lastOtpCode.IsExpired() &&
               lastOtpCode.Status == OtpCodeStatus.Unverified && maybeUser.Status == UserStatus.New)
            {
                var mail = new MailRequest(
                   ToEmail: maybeUser.Email,
                    Subject: EmailMessageExample.GetEmailSubject(),
                    Body: EmailMessageExample.GetEmailBody(maybeUser.FirstName, lastOtpCode.Code));

                await this.emailService.SendEmailAsync(mail, cancellationToken);
                return maybeUser.Id;
            }

            var newOtpCode = new OtpCode(
                OtpCodeHelper.GenerateOtpCode());

            maybeUser.OtpCodes.Add(newOtpCode);
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            var mailRequest = new MailRequest(
            ToEmail: maybeUser.Email,
            Subject: EmailMessageExample.GetEmailSubject(),
            Body: EmailMessageExample.GetEmailBody(maybeUser.FirstName, newOtpCode.Code));

            await this.emailService.SendEmailAsync(mailRequest, cancellationToken);

            return maybeUser.Id;
        }
    }
}
