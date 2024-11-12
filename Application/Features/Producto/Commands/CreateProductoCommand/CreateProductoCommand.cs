using Application.Wrappers;
using MediatR;

namespace Application.Features.Producto.Commands.CreateProductoCommand
{
    public class CreateProductoCommand : IRequest<Response<int>>
    {
        public required string Nombre { get; set; }
        public required int Capacidad { get; set; }
        public required string Unidad { get; set; }
        public required string Stock { get; set; }
        public required decimal Precio { get; set; }
        public bool EsActivo { get; set; }
    }

    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, Response<int>>
    {
        public Task<Response<int>> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
