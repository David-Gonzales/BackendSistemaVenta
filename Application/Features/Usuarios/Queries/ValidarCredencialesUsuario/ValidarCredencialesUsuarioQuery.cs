using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Queries.ValidarCredencialesUsuario
{
    public class ValidarCredencialesUsuarioQuery : IRequest<Response<SesionDto>>
    {
        public string Correo { get; set; }
        public string Clave { get; set; }

    }

    public class ValidarCredencialesUsuarioQueryHandler : IRequestHandler<ValidarCredencialesUsuarioQuery, Response<SesionDto>>
    {
        private readonly IUsuarioRepositoryAsync _usuarioRepository;

        public ValidarCredencialesUsuarioQueryHandler(IUsuarioRepositoryAsync usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Response<SesionDto>> Handle(ValidarCredencialesUsuarioQuery request, CancellationToken cancellationToken)
        {
            // Buscar usuario por correo
            var usuario = await _usuarioRepository.GetByCorreoAsync(request.Correo);
            if (usuario != null)
            {
                if (usuario.Clave != request.Clave)
                {
                    throw new ArgumentException("Credenciales incorrectas.");
                }else
                {
                    var sesionDto = new SesionDto
                    {
                        IdUsuario = usuario.Id,
                        NombreCompleto = $"{usuario.Nombres} {usuario.Apellidos}",
                        Correo = usuario.Correo,
                        Rol = usuario.Rol.Nombre
                    };
                    return new Response<SesionDto>(sesionDto);
                }                
            }
            else
            {
                throw new ArgumentException($"Usuario no encontrado");
            }
        }
    }


}
