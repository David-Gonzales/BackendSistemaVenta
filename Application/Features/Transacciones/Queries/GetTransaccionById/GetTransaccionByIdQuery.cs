using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Queries.GetTransaccionById
{
    public class GetTransaccionByIdQuery : IRequest<Response<TransaccionDto>>
    {
        public int Id { get; set; }

        public class GetTransaccionByIdQueryHandler : IRequestHandler<GetTransaccionByIdQuery, Response<TransaccionDto>>
        {
            private readonly IRepositoryAsync<Transaccion> _repositoryTransaccionAsync;
            private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;
            private readonly IRepositoryAsync<Usuario> _repositoryUsuarioAsync;

            public GetTransaccionByIdQueryHandler(IRepositoryAsync<Transaccion> repositoryTransaccionAsync, IMapper mapper, IRepositoryAsync<Producto> repositoryProductoAsync, IRepositoryAsync<Usuario> repositoryUsuarioAsync)
            {
                _repositoryTransaccionAsync = repositoryTransaccionAsync;
                _repositoryProductoAsync = repositoryProductoAsync;
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
            }

            public async Task<Response<TransaccionDto>> Handle(GetTransaccionByIdQuery request, CancellationToken cancellationToken)
            {
                var transaccion = await _repositoryTransaccionAsync.GetByIdAsync(request.Id);
                if (transaccion != null) 
                {
                    var producto = await _repositoryProductoAsync.GetByIdAsync(transaccion.IdProducto);
                    var usuario = await _repositoryUsuarioAsync.GetByIdAsync(transaccion.IdUsuario);

                    var resultado = new TransaccionDto { 
                            Id = transaccion.Id,
                            TipoTransaccion = transaccion.TipoTransaccion,
                            Fecha = transaccion.Fecha,
                            Cantidad = transaccion.Cantidad,
                            TipoEstado = transaccion.TipoEstado,

                            IdProducto = producto.Id,
                            NombreProducto = producto.Nombre,
                            CapacidadProducto = producto.Capacidad,
                            UnidadProducto = producto.Unidad,
                            StockProductoPrincipal = producto.Stock,

                            IdUsuario = usuario.Id,
                            NombreUsuario = usuario.Nombres,
                            ApellidosUsuario = usuario.Apellidos

                        };
                    return new Response<TransaccionDto>(resultado);
                }
                else
                {
                    throw new KeyNotFoundException($"Transacción no encontrada con el id {request.Id}");
                }
            }
        }
    }
}
