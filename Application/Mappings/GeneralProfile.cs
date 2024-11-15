using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Productos.Commands.CreateProductoCommand;
using Application.Features.Transacciones.Commands.CreateTransaccionCommand;
using Application.Features.Usuarios.Commands.CreateUsuarioCommand;
using Application.Features.Ventas.Commands.CreateVentaCommand;
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
            CreateMap<CreateProductoCommand, Producto>();
            CreateMap<CreateTransaccionCommand, Transaccion>();
            CreateMap<CreateUsuarioCommand, Usuario>();
            //Mapeo la Venta pero ignora los detalles en el mapeo inicial
            CreateMap<CreateVentaCommand, Venta>().ForMember(dest => dest.DetalleVentas, opt => opt.Ignore());
            #endregion

            #region DTOs
            //CreateMap<Cliente>
            #endregion 
        }
    }
}
