using Application.Features.Menus.Queries.GetAllMenus;
using Application.Features.Menus.Queries.GetMenuById;
using Application.Features.Menus.Queries.GetMenusPorUsuario;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MenuController : BaseApiController
    {
        //GET api/<controller>/
        [HttpGet("Listar")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllMenusQuery()));
        }
        //GET api/<controller>/id
        [HttpGet("Obtener")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery { Id = id }));
        }

        //GET api/<controller>/id
        [HttpGet("ListarMenusPorUsuario")]
        public async Task<IActionResult> GetMenusPorUsuario(int idUsuario)
        {
            return Ok(await Mediator.Send( new GetMenusPorUsuarioQuery { IdUsuario = idUsuario }));
        }
    }
}
