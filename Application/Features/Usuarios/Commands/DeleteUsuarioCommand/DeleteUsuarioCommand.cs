using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Commands.DeleteUsuarioCommand
{
    public class DeleteUsuarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IMapper _mapper;

        public DeleteUsuarioCommandHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _repositoryAsync.GetByIdAsync(request.Id);
            if (usuario != null)
            {
                await _repositoryAsync.DeleteAsync(usuario);
                return new Response<int>(usuario.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Usuario no encontrado con el id {request.Id}");
            }
        }
    }
}
