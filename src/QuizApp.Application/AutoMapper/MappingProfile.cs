using AutoMapper;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Tags.CreateTag;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Application.Questions.CreateQuestion;
using QuizApp.Application.Questions.UpdateQuestion;
using QuizApp.Domain.Entities;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Application.Questions.GetAllQuestions;
using QuizApp.Application.Questions.GetQuestionById;

namespace QuizApp.Application.AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateExamCommand, Exam>();
		CreateMap<CreateTagCommand,Tag>();
		CreateMap<TagResponse, Tag>();
		CreateMap<CreateQuestionCommand, Question>();
		CreateMap<UpdateQuestionCommand, Question>();
		CreateMap<Question, GetQuestionByIdResponse>();
		CreateMap<User, UserResponse>();
	}
}
