using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;

public class GetExamAttendantByExamQueryHandler :
    IQueryHandler<GetExamAttendantByExamQuery, IQueryable<ExamAttendantResponse>>
{
    private readonly IExamAttendantRepository repository;
    private readonly IMapper mapper;

    public async Task<Result<IQueryable<ExamAttendantResponse>>> Handle(GetExamAttendantByExamQuery request, CancellationToken cancellationToken)
    {
        var examAttendants = await repository.SelectAsync();
        examAttendants = examAttendants.Where(e => e.ExamId == request.ExamId);
        var response =
            (Result<IQueryable<ExamAttendantResponse>>)examAttendants
            .Select(e=>mapper.Map<ExamAttendantResponse>(e));
        return response;
    }
}

