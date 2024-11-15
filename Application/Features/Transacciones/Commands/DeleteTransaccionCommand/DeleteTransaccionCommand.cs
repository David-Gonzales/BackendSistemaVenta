using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Commands.DeleteTransaccionCommand
{
    public class DeleteTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTransaccionCommandHandler : IRequestHandler<DeleteTransaccionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Transaccion> _repositoryAsync;
        private readonly IMapper _mapper;

        public DeleteTransaccionCommandHandler(IRepositoryAsync<Transaccion> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteTransaccionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = await _repositoryAsync.GetByIdAsync(request.Id);

            if (transaccion != null)
            {
                await _repositoryAsync.DeleteAsync(transaccion);

                return new Response<int>(transaccion.Id);

            }else
            {
                throw new KeyNotFoundException($"Transacción no encontrada con el id {request.Id}");
            }
        }
    }
}
