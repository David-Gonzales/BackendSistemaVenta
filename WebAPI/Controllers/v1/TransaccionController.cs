using Application.Features.Transacciones.Commands.CreateTransaccionCommand;
using Application.Features.Transacciones.Commands.DeleteTransaccionCommand;
using Application.Features.Transacciones.Commands.UpdateTransaccionCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TransaccionController : BaseApiController
    {
        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateTransaccionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTransaccionCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTransaccionCommand { Id = id }));
        }
    }
}
