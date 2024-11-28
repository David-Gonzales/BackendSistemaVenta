using FluentValidation;

namespace Application.Features.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.Nombres)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Apellidos)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.TipoDocumento)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(11).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.NumeroDocumento)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(12).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Correo)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una dirección de email válida");

            RuleFor(p => p.Ciudad)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.")
                //9 dígitos consecutivos del (0-9)
                .Matches(@"^\d{9}").WithMessage("{PropertyName} debe ser numérico");

            RuleFor(p => p.FechaNacimiento)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.EsActivo)
                .Must(value => value == true || value == false)
                .WithMessage("{PropertyName} debe ser un valor booleano válido.");
        }
    }
}
