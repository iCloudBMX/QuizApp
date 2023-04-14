using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
namespace QuizApp.Application.Questions.GetQuestionById;
public class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, QuestionResponse>
{
    private readonly IQuestionRepository questionRepository;
    public GetQuestionByIdQueryHandler(IQuestionRepository questionRepository)
        => this.questionRepository = questionRepository;

    public async Task<Result<QuestionResponse>> Handle(
        GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        Question? questionOrEmpty = await this.questionRepository.SelectAsync(
            entityIds: request.questionId,
            cancellationToken: cancellationToken);

        if (questionOrEmpty == null)
        {
            Result.Failure<QuestionResponse>(DomainErrors.Question.NotFound(request.questionId));
        }

        var responce = new QuestionResponse(
            questionOrEmpty.Id,
            ((int)questionOrEmpty.Type),
            ((int)questionOrEmpty.Level),
            questionOrEmpty.Content,
            questionOrEmpty.CreatedAt,
            questionOrEmpty.UserId);

        return responce;
    }
}
