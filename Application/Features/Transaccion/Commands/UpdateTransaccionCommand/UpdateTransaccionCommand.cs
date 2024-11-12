using Application.Wrappers;
using MediatR;

namespace Application.Features.Transaccion.Commands.UpdateTransaccionCommand
{
    public class UpdateTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string Estado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class UpdateTransaccionCommandHandler : IRequestHandler<UpdateTransaccionCommand, Response<int>>
    {
        public Task<Response<int>> Handle(UpdateTransaccionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
