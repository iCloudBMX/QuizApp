using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Exams.CreateExam;

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
    }
}
