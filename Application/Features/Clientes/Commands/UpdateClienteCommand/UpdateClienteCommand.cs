using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string TipoDocumento { get; set; }
        public required string NumeroDocumento { get; set; }
        public required string Correo { get; set; }
        public required string Ciudad { get; set; }
        public required string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsActivo { get; set; }
    }

    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        
        public async Task<Response<int>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var registro = await _repositoryAsync.GetByIdAsync(request.Id);

            if (registro != null)
            {
                registro.Nombres = request.Nombres;
                registro.Apellidos = request.Apellidos;
                registro.TipoDocumento = request.TipoDocumento;
                registro.NumeroDocumento = request.NumeroDocumento;
                registro.Correo = request.Correo;
                registro.Ciudad = request.Ciudad;
                registro.Telefono = request.Telefono;
                registro.FechaNacimiento = request.FechaNacimiento;

                // Calcula la Edad si FechaNacimiento ha cambiado
                registro.Edad = DateTime.Now.Year - request.FechaNacimiento.Year -
                                (DateTime.Now < request.FechaNacimiento.AddYears(DateTime.Now.Year - request.FechaNacimiento.Year) ? 1 : 0);


                registro.EsActivo = request.EsActivo;

                await _repositoryAsync.UpdateAsync(registro);

                return new Response<int>(registro.Id);
            }
            else
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
        }
    }
}
