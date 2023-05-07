using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Exams.AddQuestionsByExamId
{
    public class AddQuestionsByExamIdCommandHandler : ICommandHandler<AddQuestionsByExamIdCommand, Guid>
    {
        private readonly IExamRepository examRepository;
        private readonly IUnitOfWork unitOfWork;

        public AddQuestionsByExamIdCommandHandler(
            IExamRepository examRepository,
            IUnitOfWork unitOfWork)
        {
            this.examRepository = examRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            AddQuestionsByExamIdCommand request,
            CancellationToken cancellationToken)
        {
            Guid examId = request.ExamId;
            IList<Guid> questionsIds = request.QuestionsIds;

            var maybeExam = await examRepository.SelectAsync(
                entityIds: examId,
                cancellationToken: cancellationToken);

            if (maybeExam is null)
            {
                return Result.Failure<Guid>(
                    Domain.Errors.DomainErrors.Exam.NotFound(examId));
            }
            var query = "INSERT INTO ExamQuestion (ExamId, QuestionId) VALUES ";

            query += string.Join(",", questionsIds.Select(qId => $"('{examId}', '{qId}')"));

            examRepository.ExacuteSqlQuery(query);

            await unitOfWork.SaveChangesAsync();

            return examId;
        }
    }
}
