using Application.Wrappers;
using MediatR;

namespace Application.Features.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string TipoDocumento { get; set; }
        public required string NumeroDocumento { get; set; }
        public required string Correo { get; set; }
        public required string Ciudad { get; set; }
        public required string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsActivo { get; set; }
    }

    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Response<int>>
    {
        public Task<Response<int>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
