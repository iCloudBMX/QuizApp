using FluentValidation;

namespace QuizApp.Application.OtpCodes.Validation
{
    public class VerifyOtpCodeValidator : AbstractValidator<VerifyOtpCodeCommand>
    {
        public VerifyOtpCodeValidator()
        {
            RuleFor(otp => otp.OtpCode)
                .NotNull().NotEmpty().WithMessage("OtpCode is required")
                .Length(6).WithMessage("OtpCode length must equel to 6");

            RuleFor(otp => otp.UserId)
                .NotNull().NotEmpty().WithMessage("UserId is required")
                .NotEqual(Guid.Empty).WithMessage("It does not equels default value");
        }
    }
}
