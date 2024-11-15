using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Transacciones.Commands.UpdateTransaccionCommand
{
    public class UpdateTransaccionCommandValidator : AbstractValidator<UpdateTransaccionCommand>
    {
        private readonly IRepositoryAsync<Producto> _productoRepository;
        private readonly IRepositoryAsync<Usuario> _usuarioRepository;
        public UpdateTransaccionCommandValidator(IRepositoryAsync<Producto> productoRepository, IRepositoryAsync<Usuario> usuarioRepository)
        {
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;

            // Validación para Producto
            RuleFor(p => p.IdProducto)
                .MustAsync(async (idProducto, cancellation) => await ProductoExiste(idProducto, cancellation))
                .WithMessage("El producto no existe.");

            // Validación para Usuario
            RuleFor(p => p.IdUsuario)
                .MustAsync(async (idUsuario, cancellation) => await UsuarioExiste(idUsuario, cancellation))
                .WithMessage("El usuario no existe.");

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.TipoTransaccion)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");

            RuleFor(p => p.Fecha)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");

            RuleFor(p => p.Cantidad)
                //>0
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que cero");

            RuleFor(p => p.TipoEstado)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");
            
        }

        // Método para verificar si el Producto existe
        private async Task<bool> ProductoExiste(int idProducto, CancellationToken cancellation)
        {
            return await _productoRepository.GetByIdAsync(idProducto) != null;
        }

        // Método para verificar si el Usuario existe
        private async Task<bool> UsuarioExiste(int idUsuario, CancellationToken cancellation)
        {
            return await _usuarioRepository.GetByIdAsync(idUsuario) != null;
        }
    }
}
