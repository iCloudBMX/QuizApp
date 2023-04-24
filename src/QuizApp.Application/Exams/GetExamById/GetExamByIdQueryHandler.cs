using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Exams.GetExamById;

public class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, GetExamByIdResponse>
{
    private readonly IExamRepository examRepository;

    public GetExamByIdQueryHandler(
        IExamRepository examRepository)
    {
        this.examRepository = examRepository;
    }

    public async Task<Result<GetExamByIdResponse>> Handle(
        GetExamByIdQuery request,
        CancellationToken cancellationToken)
    {
        Exam? maybeExam = await this.examRepository.SelectAsync(
            entityIds: request.examId,
            cancellationToken: cancellationToken);


        if (maybeExam is null)
        {
            return Result.Failure<GetExamByIdResponse>(
                DomainErrors.Exam.NotFound(request.examId));
        }

        var response = new GetExamByIdResponse(
            maybeExam.Id,
            maybeExam.TesterId,
            maybeExam.StartsAt,
            maybeExam.EndsAt,
            maybeExam.Duration,
            maybeExam.Link,
            maybeExam.Title,
            maybeExam.QuestionCount);

        return response;

    }
}
