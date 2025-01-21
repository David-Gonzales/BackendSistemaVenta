using Application.Features.Reportes.Queries.GetVentasPorFecha;
using Application.Features.Ventas.Queries.GetAllVentas;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReporteController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("VentasPorFecha")]
        public async Task<IActionResult> Get([FromQuery] GetVentasPorFechaParameters filter)
        {
            return Ok(await Mediator.Send(new GetVentasPorFechaQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                FechaInicio = filter.FechaInicio,
                FechaFin = filter.FechaFin
            }));
        }
    }
}
