using QuizApp.Application.Abstractions;
using QuizApp.Application.ExamAttendants.CreateExamAttendant;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.ExamAttendants;

public class CreateExamAttendantCommandHendler : ICommandHandler<CreateExamAttendantCommand, CreateExamAttendantResponse>
{
    private readonly IExamAttendantRepository repository;

    public CreateExamAttendantCommandHendler(IExamAttendantRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<CreateExamAttendantResponse>> Handle(
        CreateExamAttendantCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
