using AutoMapper;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Questions.CreateQuestion;
using QuizApp.Application.Questions.UpdateQuestion;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateExamCommand, Exam>();
		CreateMap<CreateQuestionCommand, Question>();
		CreateMap<UpdateQuestionCommand, Question>();
	}
}
