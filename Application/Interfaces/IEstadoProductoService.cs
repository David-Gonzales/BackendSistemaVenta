using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEstadoProductoService
    {
        Task<EstadoProducto> GetEstadoProductoByTipoAsync(int productoId, string tipoEstado);

        Task SetStockPorEstadoAsync(EstadoProducto estadoProducto, string tipoEstado, int nuevoStock); 

    }
}
