using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
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
            private readonly IRepositoryAsync<Usuario> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllUsuariosQueryHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<UsuarioDto>>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
            {
                var usuarios = await _repositoryAsync.ListAsync(new PagedUsuariosSpecification(request.PageSize, request.PageNumber, request.Parametros));
                var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);
                
                return new PagedResponse<List<UsuarioDto>>(usuariosDto, request.PageNumber, request.PageSize);
            }
        }
    }
}
