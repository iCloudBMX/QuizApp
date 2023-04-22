using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Questions.GetQuestionById;
public class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, GetQuestionByIdResponse>
{
    private readonly IQuestionRepository questionRepository;
    public GetQuestionByIdQueryHandler(IQuestionRepository questionRepository)
        => this.questionRepository = questionRepository;

    public async Task<Result<GetQuestionByIdResponse>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        Question? isquestion = await this.questionRepository.SelectAsync(
            cancellationToken: cancellationToken,
            entityIds: request.id);

        if (isquestion == null)
        {
            return 
                Result.Failure<GetQuestionByIdResponse>(
                DomainErrors.Question.NotFound(request.id));
        }

        var response = new GetQuestionByIdResponse(
            isquestion.Id,
            ((int)isquestion.Type),
            ((int)isquestion.Level),
            isquestion.Content);

        return response;
    }
}
