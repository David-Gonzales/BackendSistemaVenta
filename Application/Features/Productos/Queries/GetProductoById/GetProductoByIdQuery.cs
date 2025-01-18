using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Queries.GetProductoById
{
    public class GetProductoByIdQuery : IRequest<Response<ProductoDto>>
    {
        public int Id { get; set; }

        public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, Response<ProductoDto>>
        {
            private readonly IRepositoryAsync<Producto> _repositoryAsync;

            public GetProductoByIdQueryHandler(IRepositoryAsync<Producto> repositoryAsync)
            {
                this._repositoryAsync = repositoryAsync;
            }
            public async Task<Response<ProductoDto>> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
            {
                var producto = await _repositoryAsync.FirstOrDefaultAsync(new ProductoSpecification(request.Id), cancellationToken);

                if (producto != null)
                {
                    var dto = new ProductoDto
                    {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Capacidad = producto.Capacidad,
                        Unidad = producto.Unidad,
                        Precio = producto.Precio,
                        EsActivo = producto.EsActivo,
                        StockGeneral = producto.Estados.Sum(e => e.Stock) // Calcular el stock total
                    };

                    return new Response<ProductoDto>(dto);
                }
                else
                {
                    throw new KeyNotFoundException($"Producto no encontrado con el id {request.Id}");
                }
            }
        }
    }
}
