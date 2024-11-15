using Application.Features.Menus.Queries.GetMenuById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MenuController : BaseApiController
    {
        //GET api/<controller>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery { Id = id }));
        }
    }
}
