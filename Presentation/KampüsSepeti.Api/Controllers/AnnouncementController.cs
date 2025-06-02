using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using KampüsSepeti.Application.Features.Commands.PostCommands;
using KampüsSepeti.Application.Features.Queries.AnnouncementQueries;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator mediator;

        public AnnouncementController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement(CreateAnnouncementCommand command)
        {
            var values = await mediator.Send(command);
            return Ok("Duyuru Başarıyla oluşturuldu");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncement()
        {
            var value = await mediator.Send(new GetAllAnnouncementQuery());
            return Ok(value);
        }

        [HttpGet("GetAnnouncementById")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var value = await mediator.Send(new GetAnnouncementByIdQuery(id));
            return Ok(value);
        }

        [HttpGet("GetAnnouncementByUserId")]
        public async Task<IActionResult> GetAnnouncementByUserId(int id)
        {
            var value = await mediator.Send(new GetAnnouncementsByUserIdQuery(id));
            return Ok(value);
        }



        [HttpDelete]
        public async Task<IActionResult> RemoveAnnouncement(int id)
        {
            var values = await mediator.Send(new RemoveAnnouncementCommand(id));
            return Ok("Duyuru başarıyla silindi");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncementCommand command)
        {
            await mediator.Send(command);
            return Ok("Duyuru Başarıyla Güncellendi");
        }
    }
}
