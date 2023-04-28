using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.Tags.CreateTag;

public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, TagResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<CreateTagCommand> validator;

    public CreateTagCommandHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateTagCommand> validator)
    {
        this.tagRepository = tagRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<Result<TagResponse>> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            Tag mappedTag = mapper.Map<Tag>(request);
            tagRepository.Insert(mappedTag);

            TagResponse tagResponse = new TagResponse(mappedTag.Id, mappedTag.Title, mappedTag.TesterId);

            await unitOfWork.SaveChangesAsync();

            return tagResponse;
        }
        else
        {
            StringBuilder validateErrors = new StringBuilder();

            foreach(var error in result.Errors)
            {
                validateErrors.AppendLine(error.ErrorMessage);
            }

            var errors = new Error("Validation Errors ",validateErrors.ToString());
            
            return Result.Failure<TagResponse>(errors);
        }
    }
}
