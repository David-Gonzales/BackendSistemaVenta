using FluentValidation;

namespace Application.Features.Transacciones.Commands.UpdateTransaccionCommand
{
    public class UpdateTransaccionCommandValidator : AbstractValidator<UpdateTransaccionCommand>
    {
        public UpdateTransaccionCommandValidator() 
        {
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
    }
}
