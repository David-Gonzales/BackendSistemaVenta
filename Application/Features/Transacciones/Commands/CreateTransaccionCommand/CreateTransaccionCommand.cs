using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.CreateTransaccionCommand
{
    public class CreateTransaccionCommand : IRequest<Response<int>>
    {
        public required TipoTransaccion TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required TipoEstado TipoEstado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class CreateTransaccionCommandHandler : IRequestHandler<CreateTransaccionCommand, Response<int>>
    {
        public Task<Response<int>> Handle(CreateTransaccionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
