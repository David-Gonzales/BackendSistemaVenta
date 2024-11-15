using Application.Features.Usuarios.Commands.CreateUsuarioCommand;
using Application.Features.Usuarios.Commands.DeleteUsuarioCommand;
using Application.Features.Usuarios.Commands.UpdateUsuarioCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsuarioController : BaseApiController
    {
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
