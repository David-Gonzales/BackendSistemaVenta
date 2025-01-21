using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Menus.Queries.GetMenusPorUsuario
{
    public class GetMenusPorUsuarioQuery : IRequest<Response<List<MenuDto>>>
    {
        public int IdUsuario { get; set; }
        public class GetRolesPorUsuarioQueryHandler : IRequestHandler<GetMenusPorUsuarioQuery, Response<List<MenuDto>>>
        {
            private readonly IRepositoryAsync<Usuario> _repositoryUsuarioAsync;
            private readonly IRepositoryAsync<Menu> _repositoryMenuAsync;
            private readonly IRepositoryAsync<MenuRol> _repositoryMenuRolAsync;
            private readonly IMapper _mapper;

            public GetRolesPorUsuarioQueryHandler(IRepositoryAsync<Menu> repositoryMenuAsync, IMapper mapper, IRepositoryAsync<MenuRol> repositoryMenuRolAsync, IRepositoryAsync<Usuario> repositoryUsuarioAsync)
            {
                _repositoryMenuAsync = repositoryMenuAsync;
                _mapper = mapper;
                _repositoryMenuRolAsync = repositoryMenuRolAsync;
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
            }

            public async Task<Response<List<MenuDto>>> Handle(GetMenusPorUsuarioQuery request, CancellationToken cancellationToken)
            {
                var usuario = await _repositoryUsuarioAsync.GetByIdAsync(request.IdUsuario, cancellationToken);

                if (usuario == null)
                {
                    return new Response<List<MenuDto>>("Usuario no encontrado.");
                }

                // 2. Si el usuario no tiene rol, no se deben devolver menús
                if (usuario.IdRol == null || usuario.IdRol == 0)
                {
                    return new Response<List<MenuDto>>(new List<MenuDto>());
                }

                // 3. Obtener los IdMenu asociados al Rol del usuario
                var menuRoles = await _repositoryMenuRolAsync.ListAsync(cancellationToken);
                var menuRolesFiltrados = menuRoles.Where(mr => mr.IdRol == usuario.IdRol).ToList();

                if (!menuRolesFiltrados.Any())
                {
                    return new Response<List<MenuDto>>(new List<MenuDto>());
                }

                // 4. Obtener los menús correspondientes
                var menuIds = menuRolesFiltrados.Select(mr => mr.IdMenu).ToList();
                var menuSpecification = new MenuSpecification(menuIds);

                // Obtener los menús principales junto con sus submenús
                var menus = await _repositoryMenuAsync.ListAsync(menuSpecification, cancellationToken);

                // 5. Mapear los menús a DTOs
                var menuDtos = _mapper.Map<List<MenuDto>>(menus);

                return new Response<List<MenuDto>>(menuDtos);
            }
        }
    }
}
