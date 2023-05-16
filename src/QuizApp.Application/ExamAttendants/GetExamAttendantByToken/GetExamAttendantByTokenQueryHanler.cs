using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.ExamAttendants.GetExamAttendantByToken;

public class GetExamAttendantByTokenQueryHanler
    : IQueryHandler<GetExamAttendantByTokenQuery, ExamAttendantResponse>
{
    private readonly IExamAttendantRepository repository;

    public GetExamAttendantByTokenQueryHanler(IExamAttendantRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ExamAttendantResponse>> Handle(GetExamAttendantByTokenQuery request, CancellationToken cancellationToken)
    {
        var examAttendants = await repository.SelectAllAsync();

        examAttendants = examAttendants.Where(
            e => e.Token.Equals(request.token));
        var response = ExamAttendantMapper.MapToExamAttendantResponse(
            attendant: examAttendants.FirstOrDefault());

        return response;
    }
}
