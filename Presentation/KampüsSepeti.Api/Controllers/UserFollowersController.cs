using KampüsSepeti.Application.Features.Commands.PostCommands;
using KampüsSepeti.Application.Features.Commands.UserFollowersCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowersController : ControllerBase
    {

        private readonly IMediator mediator;

        public UserFollowersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateFollow(CreateUserFollowersCommand command)
        {
            var values = await mediator.Send(command);
            return Ok("Kullanıcı başarıyla takip edildi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFollow(int followerId, int followedId)
        {
            var values = await mediator.Send(new RemoveUserFollowersCommand(followedId,followerId));
            return Ok(values);
        }

    }
}
