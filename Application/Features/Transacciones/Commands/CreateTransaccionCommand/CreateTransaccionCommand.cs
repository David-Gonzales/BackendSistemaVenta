using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.CreateTransaccionCommand
{
    public class CreateTransaccionCommand : IRequest<Response<int>>
    {
        public required string TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string TipoEstado { get; set; }//Lleno o vacío
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
    }

    public class CreateTransaccionCommandHandler : IRequestHandler<CreateTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
                
        public async Task<Response<int>> Handle(CreateTransaccionCommand request, CancellationToken cancellationToken)
        {
            var nuevaTransaccion = _mapper.Map<Transaccion>(request);
            var data = await _repositoryAsync.AddAsync(nuevaTransaccion);

            return new Response<int>(data.Id);
        }
    }
}
