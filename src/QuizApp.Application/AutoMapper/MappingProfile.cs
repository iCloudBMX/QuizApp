using AutoMapper;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Tags.CreateTag;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateExamCommand, Exam>();
		CreateMap<CreateTagCommand,Tag>();
		CreateMap<TagResponse, Tag>();
	}
}
