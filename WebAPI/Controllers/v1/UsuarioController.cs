using Application.DTOs;
using Application.Features.Clientes.Queries.GetClienteById;
using Application.Features.Usuarios.Commands.CreateUsuarioCommand;
using Application.Features.Usuarios.Commands.DeleteUsuarioCommand;
using Application.Features.Usuarios.Commands.UpdateUsuarioCommand;
using Application.Features.Usuarios.Queries.GetAllUsuarios;
using Application.Features.Usuarios.Queries.GetUsuarioById;
using Application.Features.Usuarios.Queries.ValidarCredencialesUsuario;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsuarioController : BaseApiController
    {
        //GET api/<controller>
        [HttpGet("Listar")]
        public async Task<IActionResult> Get([FromQuery] GetAllUsuariosParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllUsuariosQuery
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
            return Ok(await Mediator.Send(new GetUsuarioByIdQuery { Id = id }));
        }
        //POST api/<controller>
        [HttpPost("Guardar")]
        public async Task<IActionResult> Post(CreateUsuarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/id
        [HttpPut("Editar")]
        public async Task<IActionResult> Put(UpdateUsuarioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteUsuarioCommand { Id = id }));
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDto login)
        {
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Clave))
                return BadRequest("Correo y clave son obligatorios.");

            try
            {
                // Enviar la query al Mediator
                var response = await Mediator.Send(new ValidarCredencialesUsuarioQuery
                {
                    Correo = login.Correo,
                    Clave = login.Clave
                });

                // Verificar si la respuesta tiene datos válidos
                if (response.Data == null)
                    return Unauthorized("Credenciales inválidas.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }

        }
    }
}
