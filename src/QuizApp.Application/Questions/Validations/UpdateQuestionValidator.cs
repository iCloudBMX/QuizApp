using FluentValidation;
using QuizApp.Application.Questions.UpdateQuestion;

namespace QuizApp.Application.Questions.Validations;
public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionValidator()
    {
        
    }
}
