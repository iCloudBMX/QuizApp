using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.ExamAttendants;
using QuizApp.Application.ExamAttendants.DeleteExamAttendantById;
using QuizApp.Application.ExamAttendants.GetExamAttendantByToken;
using QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;

namespace QuizApp.Api.Controllers;

[Route("api/examAttendant")]
public class ExamAttendantController : ApiController
{
    public ExamAttendantController(ISender sender,
        IServiceProvider provider) : base(sender, provider)
    {
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateExamAttendant(
        CreateExamAttendantCommand command,
        CancellationToken token)
    {
        var response = await HandleAsync<ExamAttendantResponse,
            CreateExamAttendantCommand>(command, token);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return Ok(response);
    }

    [HttpGet("examId:Guid")]
    public async ValueTask<IActionResult> GetExamAttendantByExamId(
        Guid examId,
        CancellationToken token)
    {
        var query = new GetExamAttendantByExamQuery(examId);
        var response = await HandleAsync<
            IQueryable<ExamAttendantResponse>,
            GetExamAttendantByExamQuery>(query, token);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }

    [HttpGet("token:string")]
    public async ValueTask<IActionResult> GetExamAttendantByToken(
        string token,
        CancellationToken cancellationToken)
    {
        var query = new GetExamAttendantByTokenQuery(token);

        var response = await HandleAsync<ExamAttendantResponse,
            GetExamAttendantByTokenQuery>(query, cancellationToken);

        if (response.IsFailure)
            return HandleFailure(response);

        return Ok(response);
    }
    [HttpDelete("id:Guid")]
    public async ValueTask<IActionResult> DeleteExamAttendantById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new DeleteExamAttendantByIdCommand(id);

        var response = await HandleAsync<ExamAttendantResponse,
            DeleteExamAttendantByIdCommand>(query, cancellationToken);

        if (response.IsFailure)
            return HandleFailure(response);

        return Ok(response);
    }
}