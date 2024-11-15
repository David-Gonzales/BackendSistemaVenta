using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.DeleteUsuarioCommand
{
    public class DeleteUsuarioCommandValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        public DeleteUsuarioCommandValidator() 
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");
        }
    }
}
