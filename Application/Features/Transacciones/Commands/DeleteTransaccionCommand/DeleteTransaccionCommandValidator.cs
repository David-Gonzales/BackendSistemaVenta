using FluentValidation;

namespace Application.Features.Transacciones.Commands.DeleteTransaccionCommand
{
    public class DeleteTransaccionCommandValidator : AbstractValidator<DeleteTransaccionCommand>
    {
        public DeleteTransaccionCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");
        }
    }
}
