using Application.Features.Roles.Queries.GetAllRoles;
using Application.Features.Roles.Queries.GetRolById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RolController : BaseApiController
    {
        //GET api/<controller>/
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery()));
        }
        //GET api/<controller>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetRolByIdQuery { Id = id }));
        }
    }
}
