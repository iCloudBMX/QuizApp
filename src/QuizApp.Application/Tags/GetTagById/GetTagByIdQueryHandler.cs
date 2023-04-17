using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Tags.GetTagById;

public class GetTagByIdQueryHandler : IQueryHandler<GetTagByIdQuery, TagResponse>
{
    private readonly ITagRepository tagRepository;

    public GetTagByIdQueryHandler(
        ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }

    public async Task<Result<TagResponse>> Handle(
        GetTagByIdQuery request, 
        CancellationToken cancellationToken)
    {
        Tag? maybeTag = await this.tagRepository.SelectAsync(
            entityIds:request.TagId,
            cancellationToken: cancellationToken);

        if (maybeTag is null)
            return Result.Failure<TagResponse>(
                    DomainErrors.Tag.NotFound(request.TagId));
        

        var response = new TagResponse(maybeTag.Id, maybeTag.Title,maybeTag.TesterId);

        return response;
    }
}
