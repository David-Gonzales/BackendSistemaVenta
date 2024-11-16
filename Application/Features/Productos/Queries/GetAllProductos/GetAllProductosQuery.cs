using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
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
            private readonly IMapper _mapper;
            
            public GetAllProductosQueryHandler(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<ProductoDto>>> Handle(GetAllProductosQuery request, CancellationToken cancellationToken)
            {
                //Devuelve un listado de clientes con la especificación que le pase
                var productos = await _repositoryAsync.ListAsync(new PagedProductosSpecification(request.PageSize, request.PageNumber, request.Parametros));
                var productosDto = _mapper.Map<List<ProductoDto>>(productos);

                return new PagedResponse<List<ProductoDto>>(productosDto, request.PageNumber, request.PageSize);
            }
        }
    }
}
