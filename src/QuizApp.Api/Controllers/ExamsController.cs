using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Exams.AddQuestionsByExamId;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Exams.GetExamById;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/exam")]
public class ExamsController : ApiController
{
    public ExamsController(ISender sender,IServiceProvider provider)
        : base(sender,provider)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateExam(
        CreateExamCommand exam,
        CancellationToken cancellationToken)
    {
        var response = await HandleAsync<CreateExamResponse,
            CreateExamCommand>(exam, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }

    [HttpPost("/{examId:guid}")]
    public async Task<IActionResult>AddQuestionsByExamId(
        Guid examId,
        IList<Guid>questionsIds,
        CancellationToken cancellationToken)
    {
        var query=new AddQuestionsByExamIdCommand(examId, questionsIds);

        var response=await HandleAsync<Guid,
            AddQuestionsByExamIdCommand>(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }


    [HttpGet("{examId:guid}")]
    public async Task<IActionResult> GetExamById(
        Guid examId,
        CancellationToken cancellationToken)
    {
        var query = new GetExamByIdQuery(examId);

        Result<GetExamByIdResponse> response =
            await HandleAsync<GetExamByIdResponse,
            GetExamByIdQuery>(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);

    }
}
