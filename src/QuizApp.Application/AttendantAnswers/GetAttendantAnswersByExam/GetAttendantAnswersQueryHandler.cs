using QuizApp.Application.Abstractions;
using QuizApp.Application.AttendantAnswers.GetAttendantAnswerByExam;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.AttendantAnswers.GetAttendantAnswersByExam;

public class GetAttendantAnswersQueryHandler
    : IQueryHandler<GetAttendantAnswersQuery, GetAttendantAnswersResponse>
{
    private readonly IAttendantAnswerRepository repository;
    private readonly IExamAttendantRepository examAttendantRepository;

    public async Task<Result<GetAttendantAnswersResponse>> Handle(
        GetAttendantAnswersQuery request,
        CancellationToken cancellationToken)
    {
        var attendant = examAttendantRepository
            .SelectAllAsync()
            .Where(a => a.Token == request.Token || a.ExamId == request.ExamId)
            .FirstOrDefault();
        if (attendant == null)
            return Result.Failure<GetAttendantAnswersResponse>(
                DomainErrors.ExamAttendant.NotFound((Guid)request.ExamId));

        var answers = (await repository.SelectWithQuestions())
            .Where(a => a.ExamAttendantId == attendant.Id)
            .Where(a => a.ExamId == attendant.ExamId);

        return new GetAttendantAnswersResponse(
            answers.Select(
                a => AttendantAnswerMapper.MapToResponse(a))
            .ToList());
    }
}
