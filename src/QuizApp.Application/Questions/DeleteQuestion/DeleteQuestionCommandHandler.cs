using QuizApp.Application.Abstractions;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Questions.DeleteQuestion
{
    public class DeleteQuestionCommandHandler 
        : ICommandHandler<DeleteQuestionCommand, Guid>
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IUnitOfWork unitOfWork;
        public DeleteQuestionCommandHandler(
            IQuestionRepository questionRepository,
            IUnitOfWork unitOfWork)
        {
            this.questionRepository = questionRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            DeleteQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = await this.questionRepository
                .SelectAsync(cancellationToken, request.id);

            this.questionRepository.Delete(question);

            await this.unitOfWork.SaveChangesAsync();

            return request.id;
        }
    }
}
