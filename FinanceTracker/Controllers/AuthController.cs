using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Features.Auth.CreateUser;
using FinanceTracker.Application.Features.Auth.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            var result = await _mediator.Send(new RegisterUserCommand(dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var result = await _mediator.Send(new LoginUserCommand(dto));
            return StatusCode(result.StatusCode, result);
        }
    }

}
