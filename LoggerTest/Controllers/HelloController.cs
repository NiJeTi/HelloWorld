using System.Linq;

using LoggerTest.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggerTest.Controllers
{
    [ApiController]
    [Route("")]
    public class HelloController : Controller
    {
        private readonly ILogger _logger;
            
        private readonly NameHolder _nameHolder;

        public HelloController(ILogger logger, NameHolder nameHolder)
        {
            _logger = logger;
            
            _nameHolder = nameHolder;
        }

        [HttpPost("set_name")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetName([FromQuery] string name)
        {
            _logger.Log(LogLevel.Information ,$"New name \"{name}\" requested");
            
            if (!name.All(char.IsLetterOrDigit))
            {
                _logger.Log(LogLevel.Warning, $"Recieved name \"{name}\" is invalid");
                
                return BadRequest();
            }

            _nameHolder.Name = name;

            _logger.Log(LogLevel.Information, $"New name \"{name}\" accepted");
            
            return Accepted();
        }

        [HttpGet("hello")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Hello()
        {
            _logger.Log(LogLevel.Information, $"Greeted {_nameHolder.Name}");
            
            return Ok($"Hello, {_nameHolder.Name}!");
        }
    }
}