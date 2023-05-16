using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Api.Controllers
{
    [Route("api/otps")]
    public class OtpController : ApiController
    {
        public OtpController(ISender sender, IServiceProvider serviceProvider) : 
            base(sender, serviceProvider)
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OtpController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OtpController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OtpController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OtpController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
