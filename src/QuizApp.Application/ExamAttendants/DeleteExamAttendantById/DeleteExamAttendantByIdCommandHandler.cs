using QuizApp.Application.Abstractions;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.ExamAttendants.DeleteExamAttendantById;

public class DeleteExamAttendantByIdCommandHandler :
    ICommandHandler<DeleteExamAttendantByIdCommand, ExamAttendantResponse>
{
    private readonly IExamAttendantRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteExamAttendantByIdCommandHandler(
        IExamAttendantRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<ExamAttendantResponse>> Handle(
        DeleteExamAttendantByIdCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(cancellationToken, request.id);

        if (entity == null)
            return Result.Failure<ExamAttendantResponse>(
                DomainErrors.ExamAttendant.NotFound(request.id));

        repository.Delete(entity);

        await unitOfWork.SaveChangesAsync();

        return ExamAttendantMapper.MapToExamAttendantResponse(entity);
    }
}
