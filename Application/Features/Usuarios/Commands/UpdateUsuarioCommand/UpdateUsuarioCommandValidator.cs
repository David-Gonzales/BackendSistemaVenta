using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.UpdateUsuarioCommand
{
    public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

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
