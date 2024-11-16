using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Menus.Queries.GetAllMenus
{
    public class GetAllMenusQuery : IRequest<Response<List<MenuDto>>>
    {
        public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, Response<List<MenuDto>>> 
        {
            private readonly IRepositoryAsync<Menu> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetAllMenusQueryHandler(IRepositoryAsync<Menu> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MenuDto>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
            {
                var menus = await _repositoryAsync.ListAsync();
                var menusDto = _mapper.Map<List<MenuDto>>(menus);

                return new Response<List<MenuDto>>(menusDto);
            }
        }

    }

}
