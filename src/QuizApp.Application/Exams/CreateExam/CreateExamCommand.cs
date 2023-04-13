using QuizApp.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Exams.CreateExam;

public record CreateExamCommand(
    Guid testerId, 
    DateTime startsAt, 
    DateTime endsAt, 
    DateTime duration,
    int QuestionCount) : ICommand<ExamResponse>;

