using FluentValidation;
using QuizApp.Application.Tags.CreateTag;

namespace QuizApp.Application.Tags.Validations;

public class CreateTagValidator : AbstractValidator<CreateTagCommand>
{
	public CreateTagValidator()
	{
		RuleFor(t => t.TesterId).NotEmpty().NotEqual(default(Guid));

		RuleFor(t => t.Title).NotEmpty().WithMessage("Tag title must be provided");
	}
}
