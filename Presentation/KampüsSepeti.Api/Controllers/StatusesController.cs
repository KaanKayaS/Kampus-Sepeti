using KampüsSepeti.Application.Features.Queries.LocationQueries;
using KampüsSepeti.Application.Features.Queries.StatusQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {

        private readonly IMediator mediator;

        public StatusesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var value = await mediator.Send(new GetAllStatusQuery());
            return Ok(value);
        }
    }
}
