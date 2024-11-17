using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Queries.GetUsuarioById
{
    public class GetUsuarioByIdQuery : IRequest<Response<UsuarioDto>>
    {
        public int Id { get; set; }

        public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Response<UsuarioDto>>
        {
            private readonly IRepositoryAsync<Usuario> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetUsuarioByIdQueryHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<UsuarioDto>> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
            {
                var usuario = await _repositoryAsync.GetByIdAsync(request.Id);
                if(usuario != null)
                {
                    var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                    return new Response<UsuarioDto>(usuarioDto);
                }
                else
                {
                    throw new KeyNotFoundException($"Usuario no encontrado con el id {request.Id}");
                }
                
            }
        }
    }
}
