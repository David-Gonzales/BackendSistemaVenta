using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.DeleteTransaccionCommand
{
    public class DeleteTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTransaccionCommandHandler : IRequestHandler<DeleteTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryTransaccionAsync;
        private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;
        private readonly IRepositoryAsync<EstadoProducto> _repositoryEstadoProductoAsync;
        private readonly IMapper _mapper;

        public DeleteTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryTransaccionAsync, IMapper mapper, IRepositoryAsync<Producto> repositoryProductoAsync, IRepositoryAsync<EstadoProducto> repositoryEstadoProductoAsync)
        {
            _repositoryTransaccionAsync = repositoryTransaccionAsync;
            _mapper = mapper;
            _repositoryProductoAsync = repositoryProductoAsync;
            _repositoryEstadoProductoAsync = repositoryEstadoProductoAsync;
        }

        public async Task<Response<int>> Handle(DeleteTransaccionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = await _repositoryTransaccionAsync.GetByIdAsync(request.Id);

            if (transaccion == null)
            {
                throw new KeyNotFoundException($"Transacción no encontrada con el id {request.Id}");
                
            }

            var producto = await _repositoryProductoAsync.GetByIdAsync(transaccion.IdProducto);
            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto asociado no encontrado con el id {transaccion.IdProducto}");
            }

            // Ajustar el stock según el tipo de transacción
            var estadoProducto = await _repositoryEstadoProductoAsync.FirstOrDefaultAsync(e =>
                e.IdProducto == producto.Id && e.TipoEstado == transaccion.TipoEstado);

            if (estadoProducto == null)
            {
                throw new KeyNotFoundException($"Estado de producto no encontrado para el producto con id {producto.Id}");
            }

            if (transaccion.TipoTransaccion.ToString() == "Ingreso")
            {
                estadoProducto.Stock -= transaccion.Cantidad;
            }
            else if (transaccion.TipoTransaccion.ToString() == "Salida")
            {
                estadoProducto.Stock += transaccion.Cantidad;
            }

            await _repositoryEstadoProductoAsync.UpdateAsync(estadoProducto);

            await _repositoryTransaccionAsync.DeleteAsync(transaccion);

            return new Response<int>(transaccion.Id);
            
        }
    }
}
