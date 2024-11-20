using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateVentaCommand : IRequest<Response<int>>
    {
        public required string NumeroVenta { get; set; }
        public required string TipoVenta { get; set; }
        public required string TipoPago { get; set; }
        //public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public ICollection<CreateDetalleVentaCommand> DetalleVentas { get; set; }
    }

    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Venta> _repositoryAsync;
        private readonly IRepositoryAsync<Producto> _productoRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateVentaCommandHandler(IRepositoryAsync<Venta> repositoryAsync, IMapper mapper, IRepositoryAsync<Producto> productoRepositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
            _productoRepositoryAsync = productoRepositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            var nuevaVenta = _mapper.Map<Venta>(request);
            decimal totalVenta = 0;

            foreach (var detalle in request.DetalleVentas)
            {

                if (detalle.IdProducto == 0)
                {
                    throw new ArgumentException($"Producto no encontrado con el id {detalle.IdProducto}");
                }

                var producto = await _productoRepositoryAsync.GetByIdAsync(detalle.IdProducto);

                if (producto != null)
                {
                    detalle.PrecioUnitario = producto.Precio;
                    detalle.Total = detalle.Cantidad * detalle.PrecioUnitario;
                    totalVenta += detalle.Total;
                }
                else
                {
                    throw new KeyNotFoundException($"Producto no encontrado con el id {detalle.IdProducto}");
                }
            }

            nuevaVenta.Total = totalVenta;

            nuevaVenta.DetalleVentas = _mapper.Map<ICollection<DetalleVenta>>(request.DetalleVentas);

            var data = await _repositoryAsync.AddAsync(nuevaVenta);

            return new Response<int>(data.Id);
        }
    }
}
