using Application.Features.Transacciones.Queries.GetAllTransacciones;
using Application.Features.Ventas.Commands.CreateVentaCommand;
using Application.Features.Ventas.Queries.GetAllVentas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class VentaController : BaseApiController
    {

        //GET api/<controller>
        [HttpGet("Listar")]
        public async Task<IActionResult> Get([FromQuery] GetAllVentasParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllVentasQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                NumeroVenta = filter.NumeroVenta,
                FechaInicio = filter.FechaInicio,
                FechaFin = filter.FechaFin
            }));
        }

        //POST api/<controller>
        [HttpPost("Guardar")]
        public async Task<IActionResult> Post(CreateVentaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
