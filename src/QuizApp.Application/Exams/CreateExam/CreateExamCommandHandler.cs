﻿using AutoMapper;
using FluentValidation;
using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Application.Exams.CreateExam;

public class CreateExamCommandHandler : ICommandHandler<CreateExamCommand, CreateExamResponse>
{
    private readonly IExamRepository examRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<CreateExamCommand> validator;

    public CreateExamCommandHandler(
        IExamRepository examRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CreateExamCommand> validator)
    {
        this.examRepository = examRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.validator = validator;
    }

    public async Task<Result<CreateExamResponse>> Handle(
        CreateExamCommand request,
        CancellationToken cancellationToken)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            Exam mappedExam = mapper.Map<Exam>(request);

            mappedExam.Link = "smth";
            examRepository.Insert(mappedExam);

            CreateExamResponse examResponse = new CreateExamResponse(mappedExam.Id, mappedExam.Link);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return examResponse;
        }
        else
        {
            StringBuilder validateErrors = new StringBuilder();
            foreach (var item in result.Errors)
            {
                validateErrors.AppendLine(item.ErrorMessage);
            }
            var errors = new Error("Validation error", validateErrors.ToString());

            return Result.Failure<CreateExamResponse>(errors);
        }

    }
}
