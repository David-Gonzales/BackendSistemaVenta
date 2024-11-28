using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Application.Features.Clientes.Queries.GetAllClientes;
using Application.Features.Clientes.Queries.GetClienteById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClienteController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("Listar")]
        public async Task<IActionResult> Get([FromQuery] GetAllClientesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientesQuery 
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Parametros = filter.Parametros
            }));
        }

        //GET api/<controller>/id
        [HttpGet("Obtener")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost("Guardar")]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("Editar")]
        public async Task<IActionResult> Put(UpdateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClienteCommand { Id = id }));
        }
    }
}
