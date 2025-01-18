using Application.Features.Productos.Commands.CreateProductoCommand;
using Application.Features.Productos.Commands.DeleteProductoCommand;
using Application.Features.Productos.Commands.UpdateProductoCommand;
using Application.Features.Productos.Queries.GetAllProductos;
using Application.Features.Productos.Queries.GetProductoById;
using Application.Features.Productos.Queries.GetProductosEstados;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductoController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("Listar")]
        public async Task<IActionResult> Get([FromQuery] GetAllProductosParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllProductosQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Parametros = filter.Parametros
            }));
        }

        //GET api/<controller>
        [HttpGet("ListarProductosEstados")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetProductosEstadosQuery{}));
        }

        //GET api/<controller>/id
        [HttpGet("Obtener")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductoByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost("Guardar")]
        public async Task<IActionResult> Post(CreateProductoCommand command) 
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("Editar")]
        public async Task<IActionResult> Put(UpdateProductoCommand command) 
        {
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Delete(int id) 
        {
            return Ok(await Mediator.Send(new DeleteProductoCommand { Id = id }));
        }
    }
}
