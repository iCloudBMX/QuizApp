using FluentValidation;
using QuizApp.Application.Questions.UpdateQuestion;

namespace QuizApp.Application.Questions.Validation;
public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionValidator()
    {
        RuleFor(uq => uq.Type)
            .NotEmpty()
            .WithMessage("Type should be specified");

        RuleFor(uq => uq.Level)
            .NotEmpty()
            .WithMessage("Level should be specified");

        RuleFor(uq => uq.Content)
            .NotEmpty()
            .WithMessage("Content should be specified");
    }
}
