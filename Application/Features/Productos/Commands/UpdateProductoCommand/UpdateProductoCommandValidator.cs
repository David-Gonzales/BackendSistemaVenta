using FluentValidation;

namespace Application.Features.Productos.Commands.UpdateProductoCommand
{
    public class UpdateProductoCommandValidator : AbstractValidator<UpdateProductoCommand>
    {
        public UpdateProductoCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Capacidad)
                //>0
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que cero");

            RuleFor(p => p.Unidad)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
                .MaximumLength(3).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            //RuleFor(p => p.Stock)
            //    //>=0
            //    .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} debe ser mayor que cero");

            RuleFor(p => p.Precio)
                //>0
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que cero")
                //<
                .LessThanOrEqualTo(10000).WithMessage("{PropertyName} no debe exceder de 10,000");

            RuleFor(p => p.EsActivo)
                .Must(value => value == true || value == false)
                .WithMessage("{PropertyName} debe ser un valor booleano válido.");
        }
    }
}
