using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Questions.GetAllQuestions;
public class GetAllQuestionsQueryHandler : IQueryHandler<GetAllQuestionsQuery ,GetAllQuestionsResponse>
{
    private readonly IQuestionRepository questionRepository;
    private readonly IMapper mapper;
    public GetAllQuestionsQueryHandler(
        IQuestionRepository questionRepository,
        IMapper mapper)
    {
        this.questionRepository = questionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<GetAllQuestionsResponse>> Handle(
        GetAllQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var questions = await this.questionRepository.SelectAllAsync(cancellationToken);

        var allQuestionResponse = new GetAllQuestionsResponse(new List<GetQuestionByIdResponse>());

        foreach (var question in questions)
        {
            allQuestionResponse.Allquestion
                .Add(mapper.Map<GetQuestionByIdResponse>(question));
        }

        return allQuestionResponse;
    }
}
