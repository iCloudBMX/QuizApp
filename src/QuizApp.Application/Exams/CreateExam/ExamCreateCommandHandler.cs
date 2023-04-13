using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Exams.CreateExam;

public class ExamCreateCommandHandler : ICommandHandler<CreateExamCommand, ExamResponse>
{
    private readonly IExamRepository examRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<CreateExamCommand> validator;

    public ExamCreateCommandHandler(
        IExamRepository examRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CreateExamCommand> validator)
    {
        this.examRepository = examRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.validator = validator;
    }

    public async Task<Result<ExamResponse>> Handle(
        CreateExamCommand request,
        CancellationToken cancellationToken)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {


            Exam mappedExam = mapper.Map<Exam>(request);

            mappedExam.Link = "smth";
            examRepository.Insert(mappedExam);

            ExamResponse examResponse = new ExamResponse(mappedExam.Id, mappedExam.Link);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return examResponse;
        }
        else
        {
            string displey = "";
            result.Errors.ForEach(nm => displey += nm + "\n");

        }

        return new ExamResponse(Guid.Parse("3fa85f64 - 5717 - 4562 - b3fc - 2c963f66afa6"),"problem");

    }
}
