using Application.Wrappers;
using MediatR;

namespace Application.Features.Productos.Commands.UpdateProductoCommand
{
    public class UpdateProductoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Capacidad { get; set; }
        public required string Unidad { get; set; }
        public required int Stock { get; set; }
        public required decimal Precio { get; set; }
        public bool EsActivo { get; set; }
    }

    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, Response<int>>
    {
        public Task<Response<int>> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
