using Application.Wrappers;
using MediatR;

namespace Application.Features.Transaccion.Commands.CreateTransaccionCommand
{
    public class CreateTransaccionCommand : IRequest<Response<int>>
    {
        public required string TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string Estado { get; set; }//Lleno o vacío
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
