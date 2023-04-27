using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.Questions.CreateQuestion;
public class CreateQuestionCommandHandler 
    : ICommandHandler<CreateQuestionCommand, CreateQuestionResponse>
{
    private readonly IQuestionRepository questionRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<CreateQuestionCommand> validator;
    public CreateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateQuestionCommand> validator)
    {
        this.questionRepository = questionRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<Result<CreateQuestionResponse>> Handle(
        CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var result = validator.Validate(request);
        if(result.IsValid)
        {
            Question question = mapper.Map<Question>(request);
            questionRepository.Insert(question);

            CreateQuestionResponse questionResponse =
                new CreateQuestionResponse(question.Id);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return questionResponse;
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in result.Errors)
            {
                sb.AppendLine(item.ToString());
            }
            var error = new Error("Validation.Error", sb.ToString());

            return Result.Failure<CreateQuestionResponse>(error);
        }
    }
}
