using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Exams.AddQuestionsByExamId
{
    public record AddQuestionsByExamIdCommand(
        Guid ExamId,
        IList<Guid> QuestionsIds) : ICommand<Guid>;
}
