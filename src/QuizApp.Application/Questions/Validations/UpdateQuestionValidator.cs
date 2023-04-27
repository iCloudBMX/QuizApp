using FluentValidation;
using QuizApp.Application.Questions.UpdateQuestion;

namespace QuizApp.Application.Questions.Validations;
public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionValidator()
    {
        RuleFor(uq => uq.id)
            .NotEmpty()
            .NotEqual(default(Guid));

        RuleFor(uq => uq.Type)
            .NotEmpty().WithMessage("Type is required!")
            .GreaterThanOrEqualTo(0);

        RuleFor(uq => uq.Level)
            .NotEmpty().WithMessage("Level is required!")
            .GreaterThanOrEqualTo(0);

        RuleFor(uq => uq.Content)
            .NotEmpty()
            .WithMessage("Content is required!");
    }
}
