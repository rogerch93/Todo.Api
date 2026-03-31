using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Enums;

namespace Todo.Aplication.DTOs
{
    public class TarefaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public bool IsDeleted { get; set; } 
    }

    public class CreateTarefaDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
    }

    public class UpdateTarefaDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public StatusTarefa? Status { get; set; }
        public DateTime? DataVencimento { get; set; }
    }
}
