using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Menus.Queries.GetMenuById
{
    public class GetMenuByIdQuery : IRequest<Response<MenuDto>>
    {
        public int Id { get; set; }

        public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, Response<MenuDto>>
        {
            private readonly IRepositoryAsync<Menu> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetMenuByIdQueryHandler(IRepositoryAsync<Menu> repositoryAsync, IMapper mapper)
            {
                this._repositoryAsync = repositoryAsync;
                this._mapper = mapper;
            }

            public async Task<Response<MenuDto>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
            {
                var menu = await _repositoryAsync.GetByIdAsync(request.Id);
                if (menu != null)
                {
                    var dto = _mapper.Map<MenuDto>(menu);
                    return new Response<MenuDto>(dto);
                }
                else
                {
                    throw new KeyNotFoundException($"Menú no encontrado con el id {request.Id}");
                }
            }
        }
    }
}
