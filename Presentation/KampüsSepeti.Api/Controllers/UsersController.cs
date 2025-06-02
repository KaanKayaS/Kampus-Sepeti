using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.Application.Features.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await mediator.Send(new GetAllUsersQuery());
            return Ok(user);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetUserByIdQuery(int.Parse(userId));
           
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserPassword(UpdatePasswordCommand command)
        {
            var values = await mediator.Send(command);
            return Ok(values);
        }
    }
}
