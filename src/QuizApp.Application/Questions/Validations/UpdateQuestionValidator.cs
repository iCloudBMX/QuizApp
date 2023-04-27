using FluentValidation;
using QuizApp.Application.Questions.UpdateQuestion;

namespace QuizApp.Application.Questions.Validations;
public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionValidator()
    {
        RuleFor(uq => uq.Id)
            .NotEmpty()
            .NotEqual(default(Guid));

        RuleFor(uq => uq.Content)
            .NotEmpty()
            .WithMessage("Content must be provded");

        RuleFor(uq => uq.Type)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Type must be provded");

        RuleFor(uq => uq.Level)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Level must be provded");
    }
}