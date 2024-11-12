using FluentValidation;

namespace Application.Features.Cliente.Commands.CreateClienteCommand
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator() 
        {
            //RuleFor( p => p.Nombres)
            //    .NotEmpty().WithMessage("")
            //    .MaximumLength
            //    ;
        }
    }
}
