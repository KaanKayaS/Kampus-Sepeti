using Core.Application.Features.Queries.PostQueries;
using KampüsSepeti.Application.Features.Commands.FavoritePostCommands;
using KampüsSepeti.Application.Features.Commands.LocationCommands;
using KampüsSepeti.Application.Features.Commands.PostCommands;
using KampüsSepeti.Application.Features.Queries.FavoritePostQueries;
using KampüsSepeti.Application.Features.Queries.LocationQueries;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            var value = await mediator.Send(new GetAllPostQuery());
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByLocation(int id)
        {
            var values = await mediator.Send(new GetPostByLocationIdQuery(id));
            return Ok(values);
        }

        [HttpGet("FilterPosts")]
        public async Task<IActionResult> FilterPosts([FromQuery] FilterPostsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById(int id)
        {
            var values = await mediator.Send(new GetPostByIdQuery(id));
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostByUserId(int id)
        {
            var values = await mediator.Send(new GetAllPostByUserIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostCommand command)
        {
            var values = await mediator.Send(command);
            return Ok("İlan başarıyla oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePost(int id)
        {
            var values = await mediator.Send(new RemovePostCommand(id));
            return Ok("İlan başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostCommand command)
        {
            await mediator.Send(command);
            return Ok("Post Başarıyla Güncellendi");
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoritePostCommand command)
        {
            var values = await mediator.Send(command);
            return Ok("Favoriye başarıyla eklendi");
        }


        [HttpPost]
        public async Task<IActionResult> Removefavorite(RemoveFavoritePostCommand command)
        {
            var values = await mediator.Send(command);
            return Ok("Favoriden başarıyla kaldırıldı");
        }


        [HttpGet]
        public async Task<IActionResult> GetFavoritePostsByUserId(int id)
        {
            var values = await mediator.Send(new GetFavoritePostsByUserIdQuery(id));
            return Ok(values);
        }
    }
}
