using Application.Features.Transacciones.Commands.CreateTransaccionCommand;
using Application.Features.Transacciones.Commands.DeleteTransaccionCommand;
using Application.Features.Transacciones.Commands.UpdateTransaccionCommand;
using Application.Features.Transacciones.Queries.GetAllTransacciones;
using Application.Features.Transacciones.Queries.GetTransaccionById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TransaccionController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("Listar")]
        public async Task<IActionResult> Get([FromQuery] GetAllTransaccionesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllTransaccionesQuery
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
            return Ok(await Mediator.Send(new GetTransaccionByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost("Guardar")]
        public async Task<IActionResult> Post(CreateTransaccionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("Editar")]
        public async Task<IActionResult> Put(UpdateTransaccionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTransaccionCommand { Id = id }));
        }
    }
}
