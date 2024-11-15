using Application.Features.Ventas.Commands.CreateVentaCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class VentaController : BaseApiController
    {
        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateVentaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
