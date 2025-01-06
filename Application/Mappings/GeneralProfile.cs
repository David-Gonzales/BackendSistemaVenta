using Application.DTOs;
using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Application.Features.Productos.Commands.CreateProductoCommand;
using Application.Features.Productos.Commands.DeleteProductoCommand;
using Application.Features.Productos.Commands.UpdateProductoCommand;
using Application.Features.Transacciones.Commands.CreateTransaccionCommand;
using Application.Features.Transacciones.Commands.DeleteTransaccionCommand;
using Application.Features.Transacciones.Commands.UpdateTransaccionCommand;
using Application.Features.Usuarios.Commands.CreateUsuarioCommand;
using Application.Features.Usuarios.Commands.DeleteUsuarioCommand;
using Application.Features.Usuarios.Commands.UpdateUsuarioCommand;
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
            CreateMap<UpdateClienteCommand, Cliente>();
            CreateMap<DeleteClienteCommand, Cliente>();

            CreateMap<CreateProductoCommand, Producto>();
            CreateMap<UpdateProductoCommand, Producto>();
            CreateMap<DeleteProductoCommand, Producto>();

            CreateMap<CreateTransaccionCommand, Transaccion>();
            CreateMap<UpdateTransaccionCommand, Transaccion>();
            CreateMap<DeleteTransaccionCommand, Transaccion>();


            CreateMap<CreateUsuarioCommand, Usuario>();
            CreateMap<UpdateUsuarioCommand, Usuario>();
            CreateMap<DeleteUsuarioCommand, Usuario>();

            //Mapeo la Venta pero ignora los detalles en el mapeo inicial
            //CreateMap<CreateVentaCommand, Venta>().ForMember(dest => dest.DetalleVentas, opt => opt.Ignore());

            ////Mapeo explícito la Venta los detalles en el mapeo inicial
            CreateMap<CreateVentaCommand, Venta>().ForMember(dest => dest.DetalleVentas, opt => opt.MapFrom(src => src.DetalleVentas));


            CreateMap<CreateDetalleVentaCommand, DetalleVenta>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Cantidad * src.PrecioUnitario)) // Si quieres calcular Total
            .ForMember(dest => dest.TipoEstado, opt => opt.MapFrom(src => Enum.Parse<TipoEstado>(src.TipoEstado)))
            .ForMember(dest => dest.TipoVenta, opt => opt.MapFrom(src => Enum.Parse<TipoVenta>(src.TipoVenta)));

            //Esto asegura que las propiedades TipoEstado y TipoVenta (que son enums en DetalleVenta) se conviertan correctamente desde los strings proporcionados en el comando.
            #endregion

            #region DTOs
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Producto, ProductoDto>();
            CreateMap<Rol, RolDto>();
            CreateMap<Transaccion, TransaccionDto>();
            CreateMap<Usuario, UsuarioDto>();
            #endregion 
        }
    }
}
