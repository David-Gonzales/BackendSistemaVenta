using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Commands.CreateUsuarioCommand
{
    public class CreateUsuarioCommand : IRequest<Response<int>>
    {
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Telefono { get; set; }
        public required string Correo { get; set; }
        public required string Clave { get; set; }
        public bool EsActivo { get; set; }
        public int? IdRol { get; set; }
    }
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateUsuarioCommandHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var nuevoUsuario = _mapper.Map<Usuario>(request);
            var data = await _repositoryAsync.AddAsync(nuevoUsuario);

            return new Response<int>(data.Id);
        }
    }
}
