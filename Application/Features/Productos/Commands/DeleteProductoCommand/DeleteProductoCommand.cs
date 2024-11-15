using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Commands.DeleteProductoCommand
{
    public class DeleteProductoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Producto> _repositoryAsync;
        private readonly Mapper _mapper;

        public DeleteProductoCommandHandler(IRepositoryAsync<Producto> repositoryAsync, Mapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _repositoryAsync.GetByIdAsync(request.Id);
            if(producto != null)
            {
                await _repositoryAsync.DeleteAsync(producto);
                return new Response<int>(producto.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Producto no encontrado con el id {request.Id}");
            }
        }
    }
}
