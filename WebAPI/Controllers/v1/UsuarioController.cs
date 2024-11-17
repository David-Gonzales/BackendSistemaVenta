using Application.Features.Clientes.Queries.GetClienteById;
using Application.Features.Usuarios.Commands.CreateUsuarioCommand;
using Application.Features.Usuarios.Commands.DeleteUsuarioCommand;
using Application.Features.Usuarios.Commands.UpdateUsuarioCommand;
using Application.Features.Usuarios.Queries.GetAllUsuarios;
using Application.Features.Usuarios.Queries.GetUsuarioById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsuarioController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllUsuariosParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllUsuariosQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Parametros = filter.Parametros
            }));
        }

        //GET api/<controller>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetUsuarioByIdQuery { Id = id }));
        }
        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUsuarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUsuarioCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteUsuarioCommand { Id = id }));
        }
    }
}
