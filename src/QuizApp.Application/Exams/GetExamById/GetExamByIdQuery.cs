using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Exams.GetExamById;

public record GetExamByIdQuery (Guid examId): IQuery<GetExamByIdResponse>;

