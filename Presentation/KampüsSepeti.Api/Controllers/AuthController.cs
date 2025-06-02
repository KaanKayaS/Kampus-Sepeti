using KampüsSepeti.Application.Features.Commands.LoginCommands;
using KampüsSepeti.Application.Features.Commands.RefreshTokenCommands;
using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.Application.Features.Commands.RevokeCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            await mediator.Send(new RevokeAllCommand());
            return Ok();
        }
    }
}
