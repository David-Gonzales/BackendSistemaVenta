using Application.Wrappers;
using MediatR;

namespace Application.Features.Productos.Commands.DeleteProductoCommand
{
    public class DeleteProductoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, Response<int>>
    {
        public Task<Response<int>> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
