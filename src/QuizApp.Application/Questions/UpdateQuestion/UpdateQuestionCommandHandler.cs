using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.Questions.UpdateQuestion;
public class UpdateQuestionCommandHandler
    : ICommandHandler<UpdateQuestionCommand, UpdateQuestionResponse>
{
    private readonly IQuestionRepository questionRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<UpdateQuestionCommand> validator;
    public UpdateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UpdateQuestionCommand> validator)
    {
        this.questionRepository = questionRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<Result<UpdateQuestionResponse>> Handle(
        UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var result = this.validator.Validate(request);
        if(result.IsValid)
        {
            Question question = this.mapper.Map<Question>(request);
            this.questionRepository.Update(question);

            UpdateQuestionResponse questionResponse = new UpdateQuestionResponse(
                                    Id: question.Id,
                                    Type: ((int)question.Type),
                                    Level: ((int)question.Level),
                                    Content: question.Content);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return questionResponse;
        }
        else
        {
            var errors = new StringBuilder();

            foreach (var item in result.Errors)
                errors.AppendLine(errors.ToString());

            var error = new Error("Validation.Error", errors.ToString());

            return
                Result.Failure<UpdateQuestionResponse>(error);
        }
    }
}
