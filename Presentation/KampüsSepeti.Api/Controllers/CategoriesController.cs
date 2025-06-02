using KampüsSepeti.Application.Features.Queries.CategoriesQueries;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var value = await mediator.Send(new GetAllCategoryQuery());
            return Ok(value);
        }
    }
}
