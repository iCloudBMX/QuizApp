using AutoMapper;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateExamCommand, Exam>();

	}
}
