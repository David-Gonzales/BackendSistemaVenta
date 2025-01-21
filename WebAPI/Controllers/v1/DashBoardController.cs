using Application.Features.Dashboard.Queries.GetDashboard;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DashBoardController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("Resumen")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetDashBoardQuery{}));
        }
    }
}
