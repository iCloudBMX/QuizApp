using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Exams.GetExamById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers
{
    [Route("api/exam")]
    public class ExamsController :ApiController
    {
        public ExamsController(ISender sender) 
            :base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam(
            CreateExamCommand exam,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(exam, cancellationToken);

            return response.IsSuccess? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult>GetExamById(
            Guid examId,
            CancellationToken cancellationToken)
        {
            var query=new GetExamByIdQuery(examId);

            Result<GetExamByIdResponse>response=
                await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response);

        }
    }
}
