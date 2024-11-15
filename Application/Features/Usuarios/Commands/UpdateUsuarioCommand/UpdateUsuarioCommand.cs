using Application.Wrappers;
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
        public int? IdRol { get; set; }
    }
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Response<int>>
    {
        public Task<Response<int>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
