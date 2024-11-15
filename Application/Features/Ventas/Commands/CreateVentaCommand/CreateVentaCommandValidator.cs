using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateVentaCommandValidator : AbstractValidator<CreateVentaCommand>
    {
        public CreateVentaCommandValidator() 
        {

            RuleFor(p => p.NumeroVenta)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.TipoVenta)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                //Normal o Refill
                .MaximumLength(6).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.TipoPago)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                //Efectivo, tarjeta o Contraentrega
                .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser válido.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser válido.");

            RuleForEach(x => x.DetalleVenta)
                .SetValidator(new DetalleVentaValidator()); // Validar cada detalle

        }

        public class DetalleVentaValidator : AbstractValidator<DetalleVenta>
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
