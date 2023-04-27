using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Tags.DeleteTag;

public class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand, Guid>
{
    private readonly ITagRepository tagRepository;
    private readonly IUnitOfWork unitOfWork;


    public DeleteTagCommandHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork = null)
    {
        this.tagRepository = tagRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.SelectAsync(cancellationToken, request.TagId);
        tagRepository.Delete(tag);
        await unitOfWork.SaveChangesAsync();

        return request.TagId;
    }
}
