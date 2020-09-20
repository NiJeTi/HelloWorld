using System.Linq;

using HelloWorld.Api.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Serilog;

namespace HelloWorld.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HelloController : Controller
    {
        private readonly NameHolder _nameHolder;

        private readonly IActionContextAccessor _accessor;

        public HelloController(NameHolder nameHolder, IActionContextAccessor accessor)
        {
            _nameHolder = nameHolder;
            
            _accessor = accessor;
        }

        [HttpPost("set_name")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetName([FromQuery] string name)
        {
            Log.Debug($"Requesting new name \"{name}\"");
            
            if (!name.All(char.IsLetter))
            {
                if (name == _nameHolder.LastProposedName)
                {
                    var clientIp = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
                    
                    Log.Warning($"{clientIp} is trying to set \"{name}\" name again");
                }
                
                Log.Debug($"New name \"{name}\" is not valid");

                _nameHolder.LastProposedName = name;
                
                return BadRequest();
            }

            _nameHolder.Name = name;

            Log.Information($"Accepting new name \"{name}\"");
            
            return Accepted();
        }

        [HttpGet("hello")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Hello()
        {
            Log.Information($"Greeting {_nameHolder.Name}");
            
            return Ok($"Hello, {_nameHolder.Name}!");
        }
    }
}