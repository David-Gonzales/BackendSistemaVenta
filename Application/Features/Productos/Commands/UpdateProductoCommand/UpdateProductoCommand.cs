using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Commands.UpdateProductoCommand
{
    public class UpdateProductoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Capacidad { get; set; }
        public required string Unidad { get; set; }
        public required decimal Precio { get; set; }
        public bool EsActivo { get; set; }
    }

    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Producto> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateProductoCommandHandler(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _repositoryAsync.GetByIdAsync(request.Id);

            if (producto != null)
            {
                producto.Nombre = request.Nombre;
                producto.Capacidad = request.Capacidad;
                producto.Unidad = request.Unidad;
                producto.Precio = request.Precio;
                producto.EsActivo = request.EsActivo;

                await _repositoryAsync.UpdateAsync(producto);
                return new Response<int>(producto.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Producto no encontrado con el id{request.Id}");
            }
        }
    }
}
