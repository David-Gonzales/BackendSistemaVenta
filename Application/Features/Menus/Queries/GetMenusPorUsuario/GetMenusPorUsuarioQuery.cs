using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            public GetRolesPorUsuarioQueryHandler(IRepositoryAsync<Menu> repositoryMenuAsync, IMapper mapper, IRepositoryAsync<Usuario> repositoryUsuarioAsync, IRepositoryAsync<MenuRol> repositoryMenuRolAsync)
            {
                _repositoryMenuAsync = repositoryMenuAsync;
                _mapper = mapper;
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
                _repositoryMenuRolAsync = repositoryMenuRolAsync;
            }

            public async Task<Response<List<MenuDto>>> Handle(GetMenusPorUsuarioQuery request, CancellationToken cancellationToken)
            {
                var usuario = await _repositoryUsuarioAsync.FirstOrDefaultAsync(u => u.Id == request.IdUsuario, cancellationToken: cancellationToken);

                if (usuario == null)
                {
                    return new Response<List<MenuDto>>("Usuario no encontrado");
                }

                // Obtenemos los roles de usuario y las relaciones con los menús
                var menuRoles = await _repositoryMenuRolAsync
                    .GetAllAsQueryable()
                    .Include(m => m.Menu)
                    .Where(mr => mr.IdRol == usuario.IdRol)  // Filtramos por el rol del usuario
                    .ToListAsync(cancellationToken);

                if (!menuRoles.Any())
                {
                    return new Response<List<MenuDto>>("No se encontraron menús para el rol del usuario.");
                }

                // Filtramos los menús para obtener solo los que no tienen menú padre (menús principales)
                var menus = menuRoles
                            .Select(mr => mr.Menu)
                            .Where(menu => menu != null && menu.IdMenuPadre.HasValue && menu.IdMenuPadre.Value == 0)  // Menús principales
                            .ToList();

                if (!menus.Any())
                {
                    return new Response<List<MenuDto>>("No se encontraron menús principales.");
                }

                // Cargar submenús
                foreach (var menu in menus)
                {
                    menu.Submenus = new List<Menu>();

                    var submenus = await _repositoryMenuAsync
                        .GetAllAsQueryable()
                        .Where(m => m.IdMenuPadre == menu.Id)  // Submenús del menú actual
                        .ToListAsync(cancellationToken);

                    menu.Submenus.AddRange(submenus);
                }

                // Mapear los menús a DTOs
                var menuDtos = _mapper.Map<List<MenuDto>>(menus);

                return new Response<List<MenuDto>>(menuDtos);
            }
        }
    }
}
