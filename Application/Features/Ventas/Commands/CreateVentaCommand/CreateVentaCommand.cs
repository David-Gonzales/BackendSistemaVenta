using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateVentaCommand : IRequest<Response<int>>
    {
        //public required string NumeroVenta { get; set; }
        //public required string TipoVenta { get; set; }
        public required string TipoPago { get; set; }
        //public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public ICollection<CreateDetalleVentaCommand> DetalleVentas { get; set; }
    }

    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Venta> _repositoryAsync;
        private readonly IRepositoryAsync<NumeroVenta> _numeroVentaRepositoryAsync;
        private readonly IRepositoryAsync<Producto> _productoRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateVentaCommandHandler(IRepositoryAsync<Venta> repositoryAsync, IRepositoryAsync<NumeroVenta> numeroVentaRepositoryAsync, IMapper mapper, IRepositoryAsync<Producto> productoRepositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
            _numeroVentaRepositoryAsync = numeroVentaRepositoryAsync;
            _productoRepositoryAsync = productoRepositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            var numeroVenta = await _numeroVentaRepositoryAsync.FirstOrDefaultAsync(cancellationToken); 

            if (numeroVenta == null)
            {
                numeroVenta = new NumeroVenta
                {
                    UltimoNumero = 0,
                    FechaRegistro = DateTime.Now
                };

                await _numeroVentaRepositoryAsync.AddAsync(numeroVenta);
                await _numeroVentaRepositoryAsync.SaveChangesAsync(cancellationToken);
            }

            numeroVenta.UltimoNumero++;

            //string ceros = new string('0', 5);
            //string nuevoNumeroVenta = ceros + numeroVenta.UltimoNumero;
            //nuevoNumeroVenta = nuevoNumeroVenta[^5..]; // Extrae los últimos 5 caracteres

            numeroVenta.FechaRegistro = DateTime.Now;
            await _numeroVentaRepositoryAsync.UpdateAsync(numeroVenta);
            await _numeroVentaRepositoryAsync.SaveChangesAsync(cancellationToken);

            var nuevaVenta = _mapper.Map<Venta>(request);
            nuevaVenta.NumeroVenta = numeroVenta.UltimoNumero.ToString("D5");
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

                    if (producto.Stock < detalle.Cantidad)
                    {
                        throw new InvalidOperationException(
                            $"Stock insuficiente para el producto con ID {detalle.IdProducto}. Disponible: {producto.Stock}, solicitado: {detalle.Cantidad}");
                    }

                    // Validar y ajustar el precio unitario según las reglas del negocio
                    switch (detalle.TipoVenta)
                    {
                        case "Normal":
                            if (detalle.TipoEstado == "Lleno")
                            {
                                detalle.PrecioUnitario = producto.Precio; // Precio base del producto
                            }
                            else if (detalle.TipoEstado == "Vacio" && detalle.PrecioUnitario > 0)
                            {
                                // Precio definido por el usuario - No es necesario colocar nada aquí ya que el comando CreateDetalleVentaCommand asigna automáticamente el valor de PrecioUnitario
                            }
                            else
                            {
                                throw new ArgumentException(
                                    $"El precio para un producto vacío debe ser mayor a 0. ID Producto: {detalle.IdProducto}");
                            }
                            break;

                        case "Refill":
                            if (detalle.TipoEstado == "Lleno" && detalle.PrecioUnitario > 0)
                            {
                                // Precio definido por el usuario - No es necesario colocar nada aquí ya que el comando CreateDetalleVentaCommand asigna automáticamente el valor de PrecioUnitario
                            }
                            else
                            {
                                throw new ArgumentException(
                                    $"El precio para un refill debe ser mayor a 0. ID Producto: {detalle.IdProducto}");
                            }
                            break;

                        default:
                            throw new ArgumentException($"Tipo de venta no válido: {detalle.TipoVenta}");
                    }
                    detalle.Total = detalle.Cantidad * detalle.PrecioUnitario;
                    totalVenta += detalle.Total;

                    // Reducción del stock del producto
                    producto.Stock -= detalle.Cantidad;

                    // F5
                    await _productoRepositoryAsync.UpdateAsync(producto);
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
