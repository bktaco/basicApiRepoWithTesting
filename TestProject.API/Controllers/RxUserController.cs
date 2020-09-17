using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Library.Services;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RxUserController : ControllerBase
    {
        private readonly IRxUserService _RxUserService;
        private readonly ILogger<RxUserController> _logger;

        public RxUserController(IRxUserService RxUserService, ILogger<RxUserController> logger)
        {
            _RxUserService = RxUserService;
            _logger = logger;
        }

        [HttpGet("user/{RxUserId}")]
        public IActionResult GetRxUser([FromRoute] Guid RxUserId)
        {
            _logger.LogInformation("Test");
            var RxUser = _RxUserService.GetById(RxUserId);
            if(RxUser != null)
            {
                return Ok(RxUser);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
