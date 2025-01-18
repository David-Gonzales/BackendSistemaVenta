using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Threading;

namespace Application.Features.Transacciones.Commands.UpdateTransaccionCommand
{
    public class UpdateTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string TipoEstado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class UpdateTransaccionCommandHandler : IRequestHandler<UpdateTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryTransaccionAsync;
        private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;
        private readonly IRepositoryAsync<EstadoProducto> _repositoryEstadoProductoAsync;
        private readonly IDateTimeServices _dateTimeServices;

        public UpdateTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryAsync, IRepositoryAsync<Producto> repositoryProductoAsync, IRepositoryAsync<EstadoProducto> repositoryEstadoProductoAsync, IDateTimeServices dateTimeServices)
        {
            _repositoryTransaccionAsync = repositoryAsync;
            _repositoryProductoAsync = repositoryProductoAsync;
            _repositoryEstadoProductoAsync = repositoryEstadoProductoAsync;
            _dateTimeServices = dateTimeServices;
        }

        public async Task<Response<int>> Handle(UpdateTransaccionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = await ValidarTransaccion(request);
            var cambios = DeterminarCambios(transaccion, request);

            if (cambios.HayCambios)
            {
                await ManejarCambiosStock(transaccion, cambios, request, cancellationToken);
            }

            ActualizarTransaccion(transaccion, request);
            await _repositoryTransaccionAsync.UpdateAsync(transaccion);
            await _repositoryTransaccionAsync.SaveChangesAsync();

            return new Response<int>(transaccion.Id);
        }

        private async Task<Transaccion> ValidarTransaccion(UpdateTransaccionCommand request)
        {
            var transaccion = await _repositoryTransaccionAsync.GetByIdAsync(request.Id);
            if (transaccion == null)
                throw new KeyNotFoundException("La transacción no existe.");

            if (transaccion.TipoTransaccion.ToString() != request.TipoTransaccion)
                throw new InvalidOperationException("No se permite cambiar el tipo de transacción.");

            return transaccion;
        }

        private CambiosDto DeterminarCambios(Transaccion transaccion, UpdateTransaccionCommand request)
        {
            bool cambioProducto = transaccion.IdProducto != request.IdProducto;
            bool cambioEstado = transaccion.TipoEstado.ToString() != request.TipoEstado;
            bool cambioCantidad = transaccion.Cantidad != request.Cantidad;

            return new CambiosDto
            {
                CambioProducto = cambioProducto,
                CambioEstado = cambioEstado,
                CambioCantidad = cambioCantidad,
                SoloCambioCantidad = cambioCantidad && !cambioProducto && !cambioEstado,
                HayCambios = cambioProducto || cambioEstado || cambioCantidad
            };
        }

        private async Task ManejarCambiosStock(Transaccion transaccion, CambiosDto cambios, UpdateTransaccionCommand request, CancellationToken cancellationToken)
        {
            if (cambios.SoloCambioCantidad)
            {
                var producto = await _repositoryProductoAsync.FirstOrDefaultAsync(new ProductoSpecification(transaccion.IdProducto), cancellationToken);

                if (producto == null)
                    throw new KeyNotFoundException("Producto no encontrado.");

                var estado = producto.Estados.FirstOrDefault(e => e.TipoEstado == transaccion.TipoEstado);

                if (estado == null)
                    throw new KeyNotFoundException("Estado no encontrado.");

                var diferenciaCantidad = request.Cantidad - transaccion.Cantidad;

                //var estadoActualizado = await AjustarStock(producto, transaccion.TipoEstado.ToString(), transaccion.Cantidad, transaccion.TipoTransaccion.ToString(), revertir: false);
                var estadoActualizado = await AjustarStock(producto, request.TipoEstado, diferenciaCantidad, request.TipoTransaccion);

                foreach (var est in producto.Estados)
                {
                    if (est.Id == estadoActualizado.Id)
                    {
                        est.Stock = estadoActualizado.Stock;
                    }
                }

                await _repositoryProductoAsync.UpdateAsync(producto);
                await _repositoryProductoAsync.SaveChangesAsync();
            }
            else
            {

                var productoAnterior = await _repositoryProductoAsync.FirstOrDefaultAsync(new ProductoSpecification(transaccion.IdProducto));
                var estadoAnterior = productoAnterior.Estados.FirstOrDefault(e => e.TipoEstado == transaccion.TipoEstado);

                if (productoAnterior == null)
                    throw new KeyNotFoundException("Producto actual no encontrado.");

                if (estadoAnterior == null)
                    throw new KeyNotFoundException("Estado anterior no encontrado.");

                var estadoF = await AjustarStock(productoAnterior, transaccion.TipoEstado.ToString(), transaccion.Cantidad, transaccion.TipoTransaccion.ToString(), revertir: true);

                Producto productoAActualizar = productoAnterior;
                EstadoProducto estadoAActualizar = estadoAnterior;

                if (cambios.CambioProducto)
                {
                    foreach (var estado in productoAnterior.Estados)
                    {
                        if (estado.Id == estadoF.Id)
                        {
                            estado.Stock = estadoF.Stock;
                        }
                    }

                    var nuevoProductoSpec = new ProductoSpecification(request.IdProducto);
                    productoAActualizar = await _repositoryProductoAsync.FirstOrDefaultAsync(nuevoProductoSpec, cancellationToken);

                    if (productoAActualizar == null)
                        throw new KeyNotFoundException("Nuevo producto no encontrado.");

                    estadoAActualizar = productoAActualizar.Estados.FirstOrDefault(e => e.TipoEstado.ToString() == request.TipoEstado);

                    if (estadoAActualizar == null)
                        throw new KeyNotFoundException("Nuevo estado no encontrado.");



                }
                else if (cambios.CambioEstado)
                {
                    var estadoAnteriorRevertido = await AjustarStock(productoAnterior, transaccion.TipoEstado.ToString(), transaccion.Cantidad, transaccion.TipoTransaccion.ToString(), revertir: true);

                    foreach (var estado in productoAnterior.Estados)
                    {
                        if (estado.Id == estadoAnteriorRevertido.Id)
                        {
                            estado.Stock = estadoAnteriorRevertido.Stock;
                        }
                    }

                    if (estadoAActualizar == null)
                        throw new KeyNotFoundException("Nuevo estado no encontrado.");
                    await _repositoryProductoAsync.UpdateAsync(productoAnterior);
                }

                // Aplicar el nuevo stock al producto (ya sea el anterior o el nuevo)
                var estadoProducto = await AjustarStock(productoAActualizar, request.TipoEstado, request.Cantidad, request.TipoTransaccion);
                foreach (var item in productoAActualizar.Estados)
                {
                    if (item.Id == estadoProducto.Id)
                    {
                        item.Stock = estadoProducto.Stock;
                    }
                }


                if (cambios.CambioProducto)
                {
                    await _repositoryProductoAsync.UpdateAsync(productoAnterior); //Siempre se actualiza el anterior para reflejar la reversión
                    await _repositoryProductoAsync.UpdateAsync(productoAActualizar);
                }
                else
                {
                    await _repositoryProductoAsync.UpdateAsync(productoAActualizar);
                }
            }
            await _repositoryProductoAsync.SaveChangesAsync();
        }

        private void ActualizarTransaccion(Transaccion transaccion, UpdateTransaccionCommand request)
        {

            transaccion.Cantidad = request.Cantidad;
            transaccion.IdProducto = request.IdProducto;
            transaccion.TipoEstado = Enum.Parse<TipoEstado>(request.TipoEstado);
            transaccion.IdUsuario = request.IdUsuario;
            transaccion.Fecha = TimeZoneInfo.ConvertTime(request.Fecha, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")); 
        }
        

        private async Task<EstadoProducto> AjustarStock(Producto producto, string tipoEstado, int cantidad, string tipoTransaccion, bool revertir = false)
        {
            //var estadoProducto = await _estadoProductoService.GetEstadoProductoByTipoAsync(producto.Id, tipoEstado);

            var tipoEstadoEnum = Enum.Parse<TipoEstado>(tipoEstado, ignoreCase: true);

            var estadoProducto = await _repositoryEstadoProductoAsync.FirstOrDefaultAsync(e => e.IdProducto == producto.Id && e.TipoEstado == tipoEstadoEnum);

            if (estadoProducto == null)
            {
                throw new KeyNotFoundException($"El estado '{tipoEstado}' no está definido para este producto.");
            }

            var stock = estadoProducto.Stock;

            if (tipoTransaccion == "Ingreso")
            {
                stock += revertir ? -cantidad : cantidad;
            }
            else if (tipoTransaccion == "Salida")
            {
                if (revertir)
                {
                    stock += cantidad;
                }
                else
                {
                    if (stock < cantidad)
                        throw new InvalidOperationException("Stock insuficiente.");
                    stock -= cantidad;
                }
            }
            //await _estadoProductoService.SetStockPorEstadoAsync(estadoProducto, tipoEstado, stock);

            estadoProducto.Stock = stock;

            // Aquí (EN TEORÍA) se guarda la entidad modificada en la base de datos
            //await _repositoryEstadoProductoAsync.UpdateAsync(estadoProducto);
            //await _repositoryEstadoProductoAsync.SaveChangesAsync();

            return estadoProducto;
        }
    }
    
}
