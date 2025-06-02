using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using KampüsSepeti.Application.Features.Commands.CommentCommands;
using KampüsSepeti.Application.Features.Queries.AnnouncementQueries;
using KampüsSepeti.Application.Features.Queries.CommentQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentCommand command)
        {
            var values = await mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "Yorum başarıyla oluşturuldu");
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveComment(int id)
        {
            var values = await mediator.Send(new RemoveCommentCommand(id));
            return StatusCode(StatusCodes.Status204NoContent, "Yorum başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentCommand command)
        {
            await mediator.Send(command);
            return Ok("Yorum Başarıyla Güncellendi");
        }


        [HttpGet]
        public async Task<IActionResult> GetCommentsByAnnouncementId(int id)
        {
            var value = await mediator.Send(new GetCommentsByAnnouncementIdQuery(id));
            return Ok(value);
        }

        [HttpGet("GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var value = await mediator.Send(new GetCommentByIdQuery(id));
            return Ok(value);
        }

        [HttpGet("GetLastComment")]
        public async Task<IActionResult> GetLastComment(int userId, int announcementId)
        {
            var value = await mediator.Send(new GetLastCommentQuery(userId, announcementId));
            return Ok(value);
        }
    }
}
