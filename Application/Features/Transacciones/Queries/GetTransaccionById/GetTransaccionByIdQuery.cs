using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Queries.GetTransaccionById
{
    public class GetTransaccionByIdQuery : IRequest<Response<TransaccionDto>>
    {
        public int Id { get; set; }

        public class GetTransaccionByIdQueryHandler : IRequestHandler<GetTransaccionByIdQuery, Response<TransaccionDto>>
        {
            private readonly IRepositoryAsync<Transaccion> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetTransaccionByIdQueryHandler(IRepositoryAsync<Transaccion> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TransaccionDto>> Handle(GetTransaccionByIdQuery request, CancellationToken cancellationToken)
            {
                var transaccion = await _repositoryAsync.GetByIdAsync(request.Id);
                if (transaccion != null) 
                {
                    var dto = _mapper.Map<TransaccionDto>(transaccion);
                    return new Response<TransaccionDto>(dto);
                }
                else
                {
                    throw new KeyNotFoundException($"Transacción no encontrada con el id {request.Id}");
                }
            }
        }
    }
}
