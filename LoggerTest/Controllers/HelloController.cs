using System.Linq;

using LoggerTest.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggerTest.Controllers
{
    [ApiController]
    [Route("")]
    public class HelloController : Controller
    {
        private readonly NameHolder _nameHolder;

        public HelloController(NameHolder nameHolder)
        {
            _nameHolder = nameHolder;
        }

        [HttpPost("set_name")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetName([FromQuery] string name)
        {
            if (!name.All(char.IsLetterOrDigit))
                return BadRequest();

            _nameHolder.Name = name;

            return Accepted();
        }

        [HttpGet("hello")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Hello() => Ok($"Hello, {_nameHolder.Name}!");
    }
}