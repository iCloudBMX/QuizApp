using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Exams.GetExamById;
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

        return Ok(response);
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

        return Ok(response);

    }
}
