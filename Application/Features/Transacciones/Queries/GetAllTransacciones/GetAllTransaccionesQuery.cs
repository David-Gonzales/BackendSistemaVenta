using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Queries.GetAllTransacciones
{
    public class GetAllTransaccionesQuery : IRequest<PagedResponse<List<TransaccionDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Parametros { get; set; }

        public class GetAllTransaccionesQueryHandler : IRequestHandler<GetAllTransaccionesQuery, PagedResponse<List<TransaccionDto>>>
        {
            private readonly IRepositoryAsync<Transaccion> _repositoryTransaccionAsync;
            private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;
            private readonly IRepositoryAsync<Usuario> _repositoryUsuarioAsync;

            public GetAllTransaccionesQueryHandler(IRepositoryAsync<Transaccion> repositoryTransaccionAsync, IRepositoryAsync<Producto> repositoryProductoAsync, IRepositoryAsync<Usuario> repositoryUsuarioAsync)
            {
                _repositoryTransaccionAsync = repositoryTransaccionAsync;
                _repositoryProductoAsync = repositoryProductoAsync;
                _repositoryUsuarioAsync = repositoryUsuarioAsync;
            }

            public async Task<PagedResponse<List<TransaccionDto>>> Handle(GetAllTransaccionesQuery request, CancellationToken cancellationToken)
            {
                //Devuelve un listado de transacciones con la especificación que le pase
                var transacciones = await _repositoryTransaccionAsync.ListAsync(new PagedTransaccionesSpecification(request.PageSize, request.PageNumber, request.Parametros));

                var productos = await _repositoryProductoAsync.ListAsync();
                var usuarios = await _repositoryUsuarioAsync.ListAsync();

                var resultado =
                    from T in transacciones
                    join P in productos on T.Producto.Id equals P.Id
                    join U in usuarios on T.Usuario.Id equals U.Id
                    select new TransaccionDto
                    {
                        Id = T.Id,
                        TipoTransaccion = T.TipoTransaccion.ToString(),
                        Fecha = T.Fecha,
                        Cantidad = T.Cantidad,
                        TipoEstado = T.TipoEstado.ToString(),

                        IdProducto = P.Id,
                        NombreProducto = P.Nombre,
                        CapacidadProducto = P.Capacidad,
                        UnidadProducto = P.Unidad,
                        //StockProductoPrincipal = P.Stock,

                        IdUsuario = U.Id,
                        NombreUsuario = U.Nombres,
                        ApellidosUsuario = U.Apellidos

                    };
                return new PagedResponse<List<TransaccionDto>>(resultado.ToList(), request.PageNumber, request.PageSize);
            }
        }
    }
}
