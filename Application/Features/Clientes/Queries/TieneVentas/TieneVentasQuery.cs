using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Clientes.Queries.TieneVentas
{
    public class TieneVentasQuery : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class TieneVentasQueryHandler : IRequestHandler<TieneVentasQuery, Response<bool>>
        {
            private readonly IRepositoryAsync<Venta> _ventaRepositoryAsync;

            public TieneVentasQueryHandler(IRepositoryAsync<Venta> ventaRepositoryAsync)
            {
                _ventaRepositoryAsync = ventaRepositoryAsync;
            }

            public async Task<Response<bool>> Handle(TieneVentasQuery request, CancellationToken cancellationToken)
            {
                // Creamos la especificación para obtener las ventas del cliente
                var spec = new VentasPorClienteSpecification(request.Id);

                // Usamos el repositorio para obtener la venta filtrada según la especificación
                var ventas = await _ventaRepositoryAsync.FirstOrDefaultAsync(spec, cancellationToken);

                // Si encontramos alguna venta, devolvemos true, de lo contrario false
                return new Response<bool>(ventas != null);
            }
        }
    }


}
