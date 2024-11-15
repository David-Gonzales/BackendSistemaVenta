using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Productos.Commands.CreateProductoCommand
{
    public class CreateProductoCommand : IRequest<Response<int>>
    {
        public required string Nombre { get; set; }
        public required int Capacidad { get; set; }
        public required string Unidad { get; set; }
        public required int Stock { get; set; }
        public required decimal Precio { get; set; }
        public bool EsActivo { get; set; }
    }

    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Producto> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateProductoCommandHandler(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var nuevoProducto = _mapper.Map<Producto>(request);
            var data = await _repositoryAsync.AddAsync(nuevoProducto);

            return new Response<int>(data.Id);
        }
    }
}
