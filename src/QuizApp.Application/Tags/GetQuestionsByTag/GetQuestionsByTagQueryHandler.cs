using QuizApp.Application.Abstractions;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Tags.GetQuestionsByTag;

public class GetQuestionsByTagQueryHandler :
    IQueryHandler<GetQuestionsByTagQuery, IList<GetQuestionByIdResponse>>
{
    private readonly ITagRepository tagRepository;
    public GetQuestionsByTagQueryHandler(ITagRepository tagRepository) =>    
        this.tagRepository = tagRepository;
    

    public async Task<Result<IList<GetQuestionByIdResponse>>> Handle(
        GetQuestionsByTagQuery request, 
        CancellationToken cancellationToken)
    {
        var tags = tagRepository.GetAllTagsWithQuestions();

        var tag = tags.Result.Where(t => t.Id == request.TagId).FirstOrDefault();

        var response = new List<GetQuestionByIdResponse>();
        foreach (var question in tag.Questions)
        {
            response.Add(new GetQuestionByIdResponse(
                        question.Id, 
                        (int)question.Type, 
                        (int)question.Level, 
                        question.Content));
        }

        return response;
    }
}
