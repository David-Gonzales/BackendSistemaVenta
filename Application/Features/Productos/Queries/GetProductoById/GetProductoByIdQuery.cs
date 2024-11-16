using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public GetProductoByIdQueryHandler(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
            {
                this._repositoryAsync = repositoryAsync;
                this._mapper = mapper;
            }
            public async Task<Response<ProductoDto>> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
            {
                var producto = await _repositoryAsync.GetByIdAsync(request.Id);

                if (producto != null)
                {
                    var dto = _mapper.Map<ProductoDto>(producto);
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
