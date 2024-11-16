using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Queries.GetRolById
{
    public class GetRolByIdQuery : IRequest<Response<RolDto>>
    {
        public int Id { get; set; }

        public class GetRolByIdQueryHandler : IRequestHandler<GetRolByIdQuery, Response<RolDto>>
        {
            private readonly IRepositoryAsync<Rol> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetRolByIdQueryHandler(IRepositoryAsync<Rol> repositoryAsync, IMapper mapper)
            {
                this._repositoryAsync = repositoryAsync;
                this._mapper = mapper;
            }

            public async Task<Response<RolDto>> Handle(GetRolByIdQuery request, CancellationToken cancellationToken)
            {
                var rol = await _repositoryAsync.GetByIdAsync(request.Id);

                if (rol != null)
                {
                    var dto = _mapper.Map<RolDto>(rol);
                    return new Response<RolDto>(dto);
                }
                else
                {
                    throw new KeyNotFoundException($"Rol no encontrado con el id {request.Id}");
                }
            }
        }
    }
}
