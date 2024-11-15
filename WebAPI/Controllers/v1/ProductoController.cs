using Application.Features.Productos.Commands.CreateProductoCommand;
using Application.Features.Productos.Commands.DeleteProductoCommand;
using Application.Features.Productos.Commands.UpdateProductoCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductoController : BaseApiController
    {
        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductoCommand command) 
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductoCommand command) 
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            return Ok(await Mediator.Send(new DeleteProductoCommand { Id = id }));
        }
    }
}
