using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;

public class GetExamAttendantByExamQueryHandler :
    IQueryHandler<GetExamAttendantByExamQuery, IQueryable<ExamAttendantResponse>>
{
    private readonly IExamAttendantRepository repository;

    public GetExamAttendantByExamQueryHandler(IExamAttendantRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<IQueryable<ExamAttendantResponse>>> Handle(GetExamAttendantByExamQuery request, CancellationToken cancellationToken)
    {
        var examAttendants = await repository.SelectAsync();

        examAttendants = examAttendants.Where(e => e.ExamId == request.ExamId);
        var response =
            (Result<IQueryable<ExamAttendantResponse>>)examAttendants
            .Select(e=>ExamAttendantMapper.MapToExamAttendantResponse(e));
        return response;
    }
}

