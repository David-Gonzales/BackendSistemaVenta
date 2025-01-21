using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Queries.GetAllUsuarios
{
    public class GetAllUsuariosQuery : IRequest<PagedResponse<List<UsuarioDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Parametros { get; set; }

        public class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, PagedResponse<List<UsuarioDto>>>
        {
            private readonly IRepositoryAsync<Usuario> _repositoryUsuarioAsync;
            private readonly IRepositoryAsync<Rol> _repositoryRolAsync;

            public GetAllUsuariosQueryHandler(IRepositoryAsync<Usuario> repositoryUsuarioAsync, IRepositoryAsync<Rol> repositoryRolAsync)
            {
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
                _repositoryRolAsync = repositoryRolAsync;
            }

            public async Task<PagedResponse<List<UsuarioDto>>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
            {
                int totalCount = await _repositoryUsuarioAsync.CountAsync(new UsuariosSpecification(request.Parametros));

                var usuarios = await _repositoryUsuarioAsync.ListAsync(new PagedUsuariosSpecification(request.PageSize, request.PageNumber, request.Parametros));

                var roles = await _repositoryRolAsync.ListAsync();

                var resultado =
                    from U in usuarios
                    join R in roles on U.Rol.Id equals R.Id
                    select new UsuarioDto
                    {
                        Id = U.Id,
                        Nombres = U.Nombres,
                        Apellidos = U.Apellidos,
                        Telefono = U.Telefono,
                        Correo = U.Correo,
                        Clave = U.Clave,
                        EsActivo = U.EsActivo,

                        IdRol = R.Id,
                        NombreRol = R.Nombre
                    };
                
                return new PagedResponse<List<UsuarioDto>>(resultado.ToList(), request.PageNumber, request.PageSize, totalCount);
            }
        }
    }
}
