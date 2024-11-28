using FluentValidation;

namespace Application.Features.Usuarios.Commands.CreateUsuarioCommand
{
    public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
    {
        public CreateUsuarioCommandValidator() 
        {
            RuleFor(p => p.Nombres)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Apellidos)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.")
                //9 dígitos consecutivos del (0-9)
                .Matches(@"^\d{9}").WithMessage("{PropertyName} debe ser numérico");

            RuleFor(p => p.Correo)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una dirección de email válida");

            RuleFor(p => p.Clave)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.EsActivo)
                .Must(value => value == true || value == false)
                .WithMessage("{PropertyName} debe ser un valor booleano válido.");


        }
    }
}
