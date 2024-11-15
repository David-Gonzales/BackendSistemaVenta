using Application.Wrappers;
using MediatR;

namespace Application.Features.Transacciones.Commands.DeleteTransaccionCommand
{
    public class DeleteTransaccionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTransaccionCommandHandler : IRequestHandler<DeleteTransaccionCommand, Response<int>>
    {
        public Task<Response<int>> Handle(DeleteTransaccionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
