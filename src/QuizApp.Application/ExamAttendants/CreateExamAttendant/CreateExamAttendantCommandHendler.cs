using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.ExamAttendants;

public class CreateExamAttendantCommandHendler : ICommandHandler<CreateExamAttendantCommand, ExamAttendantResponse>
{
    private readonly IExamAttendantRepository repository;
    private readonly IValidator<CreateExamAttendantCommand> validator;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateExamAttendantCommandHendler(
        IExamAttendantRepository repository,
        IValidator<CreateExamAttendantCommand> validator,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.repository = repository;
        this.validator = validator;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<ExamAttendantResponse>> Handle(
        CreateExamAttendantCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            StringBuilder mesages = new StringBuilder();
            foreach (var item in validationResult.Errors)
            {
                mesages.Append(item.ErrorMessage);
            }
            Error error = new Error("Validation error", mesages.ToString());
            return Result.Failure<ExamAttendantResponse>(error);
        }

        ExamAttendant attendant =ExamAttendantMapper.MapToExamAttendant(request);

        repository.Insert(attendant);

        await unitOfWork.SaveChangesAsync();

        var response = mapper.Map<ExamAttendantResponse>(attendant);

        return response;
    }
}
