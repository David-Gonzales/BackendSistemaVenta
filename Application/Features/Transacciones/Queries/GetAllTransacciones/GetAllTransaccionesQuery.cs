using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transacciones.Queries.GetAllTransacciones
{
    public class GetAllTransaccionesQuery : IRequest<PagedResponse<List<TransaccionDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Parametros { get; set; }

        public class GetAllTransaccionesQueryHandler : IRequestHandler<GetAllTransaccionesQuery, PagedResponse<List<TransaccionDto>>>
        {
            private readonly IRepositoryAsync<Transaccion> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllTransaccionesQueryHandler(IRepositoryAsync<Transaccion> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<TransaccionDto>>> Handle(GetAllTransaccionesQuery request, CancellationToken cancellationToken)
            {
                //Devuelve un listado de transacciones con la especificación que le pase
                var transacciones = await _repositoryAsync.ListAsync(new PagedTransaccionesSpecification(request.PageSize, request.PageNumber, request.Parametros));
                var transaccionesDto = _mapper.Map<List<TransaccionDto>>(transacciones);

                return new PagedResponse<List<TransaccionDto>>(transaccionesDto, request.PageNumber, request.PageSize);
            }
        }
    }
}
