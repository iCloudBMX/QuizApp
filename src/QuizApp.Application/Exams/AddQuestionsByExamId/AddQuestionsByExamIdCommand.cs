using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Exams.AddQuestionsByExamId
{
    public record AddQuestionsByExamIdCommand(
        Guid ExamId,
        IList<Guid> QuestionsIds) : ICommand<Guid>;
}
