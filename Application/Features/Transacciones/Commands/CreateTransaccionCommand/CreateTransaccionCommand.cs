using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.CreateTransaccionCommand
{
    public class CreateTransaccionCommand : IRequest<Response<int>>
    {
        public required string TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string TipoEstado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class CreateTransaccionCommandHandler : IRequestHandler<CreateTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryTransaccionAsync;
        private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;
        private readonly IMapper _mapper;
        public CreateTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryTransaccionAsync, IRepositoryAsync<Producto> repositoryProductoAsync, IMapper mapper)
        {
            _repositoryTransaccionAsync = repositoryTransaccionAsync;
            _mapper = mapper;
            _repositoryProductoAsync = repositoryProductoAsync;
        }
                
        public async Task<Response<int>> Handle(CreateTransaccionCommand request, CancellationToken cancellationToken)
        {
            // Obtengo el producto asociado
            var producto = await _repositoryProductoAsync.FirstOrDefaultAsync(new ProductoSpecification(request.IdProducto), cancellationToken);
            if (producto == null) 
            { 
                throw new KeyNotFoundException($"Producto no encontrado con el id {request.IdProducto}"); 
            }

            // Busco el estado correspondiente
            var estado = producto.Estados.FirstOrDefault(e => e.TipoEstado.ToString() == request.TipoEstado);
            if (estado == null)
            {
                throw new KeyNotFoundException($"Estado '{request.TipoEstado}' no encontrado en el producto");
            }

            // Ajusto el stock según el tipo de transacción
            if (request.TipoTransaccion == "Ingreso")
            {
                estado.Stock += request.Cantidad;
            }
            else if (request.TipoTransaccion == "Salida")
            {
                if (estado.Stock < request.Cantidad)
                {
                    throw new InvalidOperationException($"Stock insuficiente para realizar la salida. Stock actual: {estado.Stock}, Cantidad solicitada: {request.Cantidad}");
                }

                estado.Stock -= request.Cantidad;
            }
            else
            {
                throw new ArgumentException("Tipo de transacción inválido. Debe ser 'Ingreso' o 'Salida'.");
            }

            await _repositoryProductoAsync.UpdateAsync(producto);

            var nuevaTransaccion = _mapper.Map<Transaccion>(request);
            var data = await _repositoryTransaccionAsync.AddAsync(nuevaTransaccion);

            return new Response<int>(data.Id);
        }
    }
}
