using Application.Features.Clientes.Commands.CreateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    //Clase de Mapeo
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region Commands
            CreateMap<CreateClienteCommand, Cliente>();
            #endregion

            #region DTOs
            //CreateMap<Cliente>
            #endregion 
        }
    }
}
