using QuizApp.Domain.Entities;

namespace QuizApp.Application.AttendantAnswers;

public record AttendantAnswerResponse(
    Guid Id,
    Guid ExamAttendantId,
    Guid QuestionId,
    Guid OptionId,
    Guid ExamId,                        
    Question Question,
    QuestionOption Option);