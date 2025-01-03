using Domain.Entities;
using FluentValidation;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateVentaCommandValidator : AbstractValidator<CreateVentaCommand>
    {
        public CreateVentaCommandValidator() 
        {

            //RuleFor(p => p.NumeroVenta)
            //   .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            //RuleFor(p => p.TipoVenta)
            //    .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
            //    //Normal o Refill
            //    .MaximumLength(6).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.TipoPago)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                //Efectivo, tarjeta o Contraentrega
                .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser válido.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser válido.");

            RuleForEach(x => x.DetalleVentas)
                .SetValidator(new DetalleVentaValidator()); // Validar cada detalle

        }

        public class DetalleVentaValidator : AbstractValidator<CreateDetalleVentaCommand>
        {
            public DetalleVentaValidator()
            {
                RuleFor(x => x.Cantidad)
                    .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que 0.");

                RuleFor(x => x.PrecioUnitario)
                    .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que 0.");
            }
        }
    }
}
