using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosQuery : IRequest<PagedResponse<List<ProductoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Parametros { get; set; }

        public class GetAllProductosQueryHandler : IRequestHandler<GetAllProductosQuery, PagedResponse<List<ProductoDto>>>
        {
            private readonly IRepositoryAsync<Producto> _repositoryAsync;
            
            public GetAllProductosQueryHandler(IRepositoryAsync<Producto> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<PagedResponse<List<ProductoDto>>> Handle(GetAllProductosQuery request, CancellationToken cancellationToken)
            {
                // Cuenta total de registros antes de aplicar paginación
                int totalCount = await _repositoryAsync.CountAsync(new ProductosSpecification(request.Parametros));

                //Devuelve un listado de clientes con la especificación que le pase
                var productos = await _repositoryAsync.ListAsync(new PagedProductosSpecification(request.PageSize, request.PageNumber, request.Parametros));

                var productosDto = productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Capacidad = p.Capacidad,
                    Unidad = p.Unidad,
                    Precio = p.Precio,
                    EsActivo = p.EsActivo,
                    StockGeneral = p.Estados?.Sum(e => e.Stock) ?? 0 // Calcular el stock total sumando los valores de stock de los estados y si Estados es null, usar 0 como valor por defecto
                }).ToList();

                return new PagedResponse<List<ProductoDto>>(productosDto, request.PageNumber, request.PageSize, totalCount);
            }
        }
    }
}
