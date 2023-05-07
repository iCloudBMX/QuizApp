using FluentValidation;
using QuizApp.Application.Questions.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Questions.Validations;
public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionValidator()
    {
        RuleFor(q => q.Type)
            .NotEmpty().WithMessage("Question type should be specified")
            .GreaterThanOrEqualTo(1);

        RuleFor(q => q.Level)
            .NotEmpty().WithMessage("Question level should be specified")
            .GreaterThanOrEqualTo(1);

        RuleFor(q => q.Content)
            .NotEmpty()
            .WithMessage("Content should be specified");

        RuleFor(q => q.UserId)
            .NotEmpty()
            .NotEqual(default(Guid));
    }
}
