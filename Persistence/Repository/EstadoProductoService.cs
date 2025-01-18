using Application.Interfaces;
using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class EstadoProductoService : IEstadoProductoService
    {
        private readonly IRepositoryAsync<EstadoProducto> _repositoryEstadoProductoAsync;

        public EstadoProductoService(IRepositoryAsync<EstadoProducto> repositoryEstadoProductoAsync)
        {
            _repositoryEstadoProductoAsync = repositoryEstadoProductoAsync;
        }
        public async Task<EstadoProducto> GetEstadoProductoByTipoAsync(int idProducto, string tipoEstado)
        {
            var tipoEstadoEnum = Enum.Parse<TipoEstado>(tipoEstado, ignoreCase: true);

            //var estadoProducto = await _context.EstadoProductos.AsNoTracking().FirstOrDefaultAsync(e => e.IdProducto == idProducto && e.TipoEstado == tipoEstadoEnum);

            var estadoProducto = await _repositoryEstadoProductoAsync.FirstOrDefaultAsync(e => e.IdProducto == idProducto && e.TipoEstado == tipoEstadoEnum);

            if (estadoProducto == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado '{tipoEstado}' para el producto.");
            }

            return estadoProducto;
        }

        public async Task SetStockPorEstadoAsync(EstadoProducto estadoProducto, string tipoEstado, int nuevoStock)
        {
            if (nuevoStock < 0)
            {
                throw new ArgumentException("El stock no puede ser un valor negativo.", nameof(nuevoStock));
            }

            //var tipoEstadoEnum = Enum.Parse<TipoEstado>(tipoEstado, ignoreCase: true);

            //var estado = await _context.EstadoProductos.AsNoTracking().FirstOrDefaultAsync(e => e.IdProducto == producto.Id && e.TipoEstado == tipoEstadoEnum);


            //if (estado == null)
            //{
            //    throw new KeyNotFoundException($"El estado '{tipoEstado}' no está definido para este producto.");
            //}

            estadoProducto.Stock = nuevoStock;

            // Aquí (EN TEORÍA) se guarda la entidad modificada en la base de datos
            await _repositoryEstadoProductoAsync.UpdateAsync(estadoProducto);
        }
    }
}
