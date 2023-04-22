using FluentValidation;

namespace QuizApp.Application.ExamAttendants.Validation;

public class ExamAttendantValidator:AbstractValidator<CreateExamAttendantCommand>
{
    public ExamAttendantValidator()
    {
        RuleFor(attendant => attendant)
            .NotNull();
        RuleFor(a=>a.ExamId)
            .NotNull()
            .NotEmpty()
            .NotEqual(default(Guid));
        RuleFor(a => a.Name)
            .NotNull()
            .NotEmpty();
    }
}
