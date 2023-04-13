using FluentValidation;
using QuizApp.Application.Exams.CreateExam;

namespace QuizApp.Application.Exams.Validation
{
    public class ExamValidator : AbstractValidator<CreateExamCommand>
    {
        public ExamValidator()
        {
            RuleFor(e => e.testerId).NotEmpty().NotEqual(default(Guid));

            RuleFor(e => e.startsAt).NotEmpty()
                .WithMessage("Start date and time must be provided")
                .Must(st => st > DateTime.Now)
                .WithMessage("Invalid date and time");

            RuleFor(e => e.endsAt).NotEmpty()
                .WithMessage("Start date and time must be provided")
                .GreaterThan(e => e.startsAt)
                .WithMessage("End date and time must be after start date and time.");

            RuleFor(e => e.QuestionCount)
                .NotEmpty()
                .WithMessage("Count must be provded")
                .GreaterThanOrEqualTo(0);
        }
    }
}
