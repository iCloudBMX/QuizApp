using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Tags.GetTagsByTesterId;

public class GetTagsByTesterIdQueryHandler : 
    IQueryHandler<GetTagsByTesterIdQuery, IList<TagResponse>>
{
    private readonly ITagRepository tagRepository;

    public GetTagsByTesterIdQueryHandler(ITagRepository tagRepository) =>
        this.tagRepository = tagRepository;
    

    public async Task<Result<IList<TagResponse>>> Handle(
        GetTagsByTesterIdQuery request, 
        CancellationToken cancellationToken)
    {
        var tags = await tagRepository.SelectAllAsync();

        tags = tags.Where(t => t.TesterId == request.TesterId).ToList();

        var response = new List<TagResponse>();

        foreach (var tag in tags)
        {
            response.Add(new TagResponse(tag.Id, tag.Title,tag.TesterId));
        }

        return response;
    }
}
