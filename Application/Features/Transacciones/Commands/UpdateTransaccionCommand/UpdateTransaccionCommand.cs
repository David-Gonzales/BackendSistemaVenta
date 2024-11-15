using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.UpdateTransaccionCommand
{
    public class UpdateTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required TipoTransaccion TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required TipoEstado TipoEstado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class UpdateTransaccionCommandHandler : IRequestHandler<UpdateTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        
        public async Task<Response<int>> Handle(UpdateTransaccionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = await _repositoryAsync.GetByIdAsync(request.Id);
            
            if (transaccion != null) 
            {
                transaccion.TipoTransaccion = request.TipoTransaccion;
                transaccion.Fecha = request.Fecha;
                transaccion.Cantidad = request.Cantidad;
                transaccion.TipoEstado = request.TipoEstado;
                transaccion.IdProducto = request.IdProducto;
                transaccion.IdUsuario = request.IdUsuario;

                await _repositoryAsync.UpdateAsync(transaccion);

                return new Response<int>(transaccion.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Transacción no encontrada con el id {request.Id}");
            }
        }
    }
}
