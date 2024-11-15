using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateVentaCommand : IRequest<Response<int>>
    {
        public string? NumeroVenta { get; set; }
        public required string TipoVenta { get; set; }
        public required string TipoPago { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public ICollection<DetalleVenta> DetalleVenta { get; set; }
    }

    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, Response<int>>
    {
        public Task<Response<int>> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
