using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Commands.UpdateUsuarioCommand
{
    public class UpdateUsuarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Telefono { get; set; }
        public required string Correo { get; set; }
        public required string Clave { get; set; }
        public bool EsActivo { get; set; }
        public int IdRol { get; set; }
    }
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateUsuarioCommandHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _repositoryAsync.GetByIdAsync(request.Id);

            if (usuario != null)
            {
                usuario.Nombres = request.Nombres;
                usuario.Apellidos = request.Apellidos;
                usuario.Telefono = request.Telefono;
                usuario.Correo = request.Correo;
                usuario.Clave = request.Clave;
                usuario.EsActivo = request.EsActivo;
                usuario.IdRol = request.IdRol;

                await _repositoryAsync.UpdateAsync(usuario);

                return new Response<int>(usuario.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Usuario no encontrado con el id {request.Id}");
            }
        }
    }
}
