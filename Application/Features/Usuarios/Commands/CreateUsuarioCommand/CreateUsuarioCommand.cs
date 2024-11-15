using Application.Wrappers;
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
        public Task<Response<int>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
