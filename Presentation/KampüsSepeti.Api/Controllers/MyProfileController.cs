using KampüsSepeti.Application.Features.Commands.MyProfileCommands;
using KampüsSepeti.Application.Features.Queries.MyProfileQueries;
using KampüsSepeti.Application.Features.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyProfileController : ControllerBase
    {

        private readonly IMediator mediator;

        public MyProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTruePhoneVisibility(int id)
        {
            var values = await mediator.Send(new UpdateTruePhoneVisibleCommand(id));
            return Ok(values);
        }

        [HttpPost("UpdateFalsePhoneVisibility")]
        public async Task<IActionResult> UpdateFalsePhoneVisibility(int id)
        {
            var values = await mediator.Send(new UpdateFalsePhoneVisibleCommand(id));
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfilePhoto(UpdateProfilePhotoCommand command)
        {
            var values = await mediator.Send(command);
            return Ok(values);
        }

        [HttpPut("UpdateNullProfilePhoto")]
        public async Task<IActionResult> UpdateNullProfilePhoto(int id)
        {
            var values = await mediator.Send(new UpdateNullProfilePhotoCommand(id));
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetPath(int id)
        {
            var values = await mediator.Send(new GetImagePathQuery(id));
            return Ok(values);
        }
    }
}
