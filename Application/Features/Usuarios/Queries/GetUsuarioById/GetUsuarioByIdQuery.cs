using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Queries.GetUsuarioById
{
    public class GetUsuarioByIdQuery : IRequest<Response<UsuarioDto>>
    {
        public int Id { get; set; }

        public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Response<UsuarioDto>>
        {
            private readonly IRepositoryAsync<Usuario> _repositoryUsuarioAsync;
            private readonly IRepositoryAsync<Rol> _repositoryRolAsync;

            public GetUsuarioByIdQueryHandler(IRepositoryAsync<Usuario> repositoryUsuarioAsync, IRepositoryAsync<Rol> repositoryRolAsync)
            {
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
                _repositoryRolAsync = repositoryRolAsync;
            }

            public async Task<Response<UsuarioDto>> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
            {
                var usuario = await _repositoryUsuarioAsync.GetByIdAsync(request.Id);
                if(usuario != null)
                {
                    var rol = await _repositoryRolAsync.GetByIdAsync(usuario.IdRol);

                    var resultado = new UsuarioDto {
                        Id = usuario.Id,
                        Nombres = usuario.Nombres,
                        Apellidos = usuario.Apellidos,
                        Telefono = usuario.Telefono,
                        Correo = usuario.Correo,
                        Clave = usuario.Clave,
                        EsActivo = usuario.EsActivo,

                        IdRol = rol.Id,
                        NombreRol = rol.Nombre

                    };

                    return new Response<UsuarioDto>(resultado);
                }
                else
                {
                    throw new KeyNotFoundException($"Usuario no encontrado con el id {request.Id}");
                }
                
            }
        }
    }
}
