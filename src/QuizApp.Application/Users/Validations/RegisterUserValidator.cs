using FluentValidation;
using QuizApp.Application.Users.Register;

namespace QuizApp.Application.Users.Validations
{
    internal class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Email)
                .NotNull().NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not in the correct format")
                .Length(5, 100).WithMessage("Email must be between 5 and 100 characters");

            RuleFor(user => user.Password)
                .NotNull().NotEmpty().WithMessage("Password is required")
                .Length(8, 50).WithMessage("Password must be between 8 and 50 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

            RuleFor(user => user.PhoneNumber)
                .NotNull().NotEmpty().WithMessage("Number is required");

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.");

            RuleFor(user => user.LastName)
               .NotEmpty().WithMessage("First name is required.")
               .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.")
               .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.");
        }
    }
}
