using KampüsSepeti.Application.Features.Queries.UniversityQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public UniversitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUniversities()
        {
            var value = await mediator.Send(new GetAllUniversityQuery());
            return Ok(value);
        }
    }
}
