using KampüsSepeti.Application.Features.Commands.LocationCommands;
using KampüsSepeti.Application.Features.Queries.LocationQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LocationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocation()
        {
            var value = await mediator.Send(new GetAllLocationQuery());
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdLocation(int id)
        {
            var value = await mediator.Send(new GetLocationByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationCommand command)
        {
            await mediator.Send(command);
            return Ok("Location Başarıyla oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            await mediator.Send(new RemoveLocationCommand(id));
            return Ok("Location Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(UpdateLocationCommand command)
        {
            await mediator.Send(command);
            return Ok("Location Başarıyla Güncellendi");
        }
    }
}
