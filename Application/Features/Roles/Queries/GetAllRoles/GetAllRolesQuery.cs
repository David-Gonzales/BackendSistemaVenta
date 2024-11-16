using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<Response<List<RolDto>>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<List<RolDto>>>
        {
            private readonly IRepositoryAsync<Rol> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllRolesQueryHandler(IRepositoryAsync<Rol> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

            public async Task<Response<List<RolDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                var roles = await _repositoryAsync.ListAsync();
                var rolesDto = _mapper.Map<List<RolDto>>(roles);

                return new Response<List<RolDto>>(rolesDto);
            }
        }
    }
}
