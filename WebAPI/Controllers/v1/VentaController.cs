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
            // Valores por defecto si no llegan parámetros
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageSize > 0 ? filter.PageSize : 10;
            filter.BuscarPor ??= string.Empty; // Evitar nulos
            filter.NumeroVenta ??= string.Empty;
            //filter.FechaInicio = filter.FechaInicio == DateTime.MinValue ? DateTime.Now.AddMonths(-1) : filter.FechaInicio;
            //filter.FechaFin = filter.FechaFin == DateTime.MinValue ? DateTime.Now : filter.FechaFin;
            filter.FechaInicio ??= string.Empty;
            filter.FechaFin ??= string.Empty;

            return Ok(await Mediator.Send(new GetAllVentasQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                BuscarPor = filter.BuscarPor,
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
