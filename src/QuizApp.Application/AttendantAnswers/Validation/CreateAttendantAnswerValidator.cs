using FluentValidation;
using QuizApp.Application.AttendantAnswers.CreateAttendantAnswer;

namespace QuizApp.Application.AttendantAnswers.Validation;

internal class CreateAttendantAnswerValidator
    : AbstractValidator<CreateAttendantAnswerCommand>
{
    public CreateAttendantAnswerValidator()
    {
        RuleFor(x => x.ExamAttendantId)
            .NotNull()
            .NotEqual(default(Guid))
            .WithMessage("Exam attendant ID is required.");
        RuleFor(x => x.QuestionId)
            .NotNull()
            .NotEqual(default(Guid))
            .WithMessage("Question ID is required.");
        RuleFor(x => x.OptionId)
            .NotNull()
            .NotEqual(default(Guid))
            .WithMessage("Option ID is required.");
        RuleFor(x => x.ExamId)
            .NotNull()
            .NotEqual(default(Guid))
            .WithMessage("Exam ID is required.");
    }
}
