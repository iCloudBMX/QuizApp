using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.AttendantAnswers.CreateAttendantAnswer;

public class CreateAttendantAnswerCommandHandler
    : ICommandHandler<CreateAttendantAnswerCommand, Guid>
{
    private readonly IAttendantAnswerRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public CreateAttendantAnswerCommandHandler(
        IAttendantAnswerRepository repository,
        IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateAttendantAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = AttendantAnswerMapper.MapToAnswer(request);

        var storageAnswer = repository.SelectAllAsync()
            .Where(a => a.ExamAttendantId == answer.ExamAttendantId)
            .Where(a => a.ExamId == answer.ExamId)
            .Where(a => a.QuestionId == answer.QuestionId)
            .FirstOrDefault();
        if (storageAnswer is null)
            repository.Insert(answer);
        else
        {
            storageAnswer.OptionId=answer.OptionId;
            repository.Update(storageAnswer);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return answer.Id;
    }
}
