using Application.Wrappers;
using MediatR;

namespace Application.Features.Usuarios.Commands.DeleteUsuarioCommand
{
    public class DeleteUsuarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Response<int>>
    {
        public Task<Response<int>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
