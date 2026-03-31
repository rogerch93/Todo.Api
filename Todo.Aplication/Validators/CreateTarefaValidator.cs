using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Aplication.DTOs;

namespace Todo.Aplication.Validators
{
    public class CreateTarefaValidator : AbstractValidator<CreateTarefaDto>
    {
        public CreateTarefaValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(200).WithMessage("Título deve ter no máximo 200 caracteres");
        }
    }
}
