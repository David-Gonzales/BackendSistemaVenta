using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Queries.GetProductosEstados
{
    public class GetProductosEstadosQuery : IRequest<Response<List<ProductoConEstadoDto>>>
    {
        public string? Parametros { get; set; }
        public class GetProductosEstadosQueryHandler : IRequestHandler<GetProductosEstadosQuery, Response<List<ProductoConEstadoDto>>>
        {
            private readonly IReadRepositoryAsync<Producto> _repositoryProductoAsync;

            public GetProductosEstadosQueryHandler(IReadRepositoryAsync<Producto> repositoryProductoAsync)
            {
                _repositoryProductoAsync = repositoryProductoAsync;
            }

            public async Task<Response<List<ProductoConEstadoDto>>> Handle(GetProductosEstadosQuery request, CancellationToken cancellationToken)
            {
                var productos = await _repositoryProductoAsync.ListAsync(new ProductosSpecification(request.Parametros), cancellationToken);

                if (productos == null || !productos.Any())
                {
                    return new Response<List<ProductoConEstadoDto>>("No se encontraron productos.");
                }

                var productosConEstadoDtos = productos.Select(
                    producto => new ProductoConEstadoDto
                    {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Estados = producto.Estados.Select(
                            estado => new EstadoProductoSimpleDto
                            {
                                TipoEstado = estado.TipoEstado.ToString(), // Convertir el enum a string
                                Stock = estado.Stock
                            }).ToList() // Convertir a lista para que coincida con el tipo del DTO
                    }).ToList();

                return new Response<List<ProductoConEstadoDto>>(productosConEstadoDtos);
            }
        }
    }
}
