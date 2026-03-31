using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string? Descricao { get; private set; }
        public StatusTarefa Status { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public DateTime? DataVencimento { get; private set; }  

        public bool IsDeleted { get; private set; }            
        public DateTime? DataExclusao { get; private set; }     

        // Construtor protegido para EF Core
        protected Tarefa() { }

        public Tarefa(string titulo)
            : this(titulo, null, null)
        {
        }

        public Tarefa(string titulo, string? descricao)
            : this(titulo, descricao, null)
        {
        }

        public Tarefa(string titulo, string? descricao, DateTime? dataVencimento)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório", nameof(titulo));

            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = StatusTarefa.Pendente;        // Status inicial sempre Pendente
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = null;
        }

        // Métodos de domínio (regras de negócio)
        public void Iniciar()
        {
            if (Status == StatusTarefa.Concluido)
                throw new InvalidOperationException("Não é possível iniciar uma tarefa já concluída.");

            if (Status != StatusTarefa.Pendente)
                throw new InvalidOperationException($"Não é possível iniciar uma tarefa no status atual: {Status}");

            Status = StatusTarefa.EmAndamento;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Concluir()
        {
            if (Status == StatusTarefa.Concluido)
                return;

            Status = StatusTarefa.Concluido;
            DataConclusao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Reabrir()
        {
            if (Status != StatusTarefa.Concluido)
                throw new InvalidOperationException("Só é possível reabrir tarefas concluídas.");

            Status = StatusTarefa.Pendente;
            DataConclusao = null;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Atualizar(string titulo, string? descricao, DateTime? dataVencimento)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório", nameof(titulo));

            Titulo = titulo;
            Descricao = descricao;
            if (dataVencimento.HasValue)
                DataVencimento = dataVencimento.Value.ToUniversalTime();

            DataAtualizacao = DateTime.UtcNow;
        }

        // Método auxiliar para transição de status (útil no futuro)
        public void AlterarStatus(StatusTarefa novoStatus)
        {
            if (novoStatus == Status) return;

            switch (novoStatus)
            {
                case StatusTarefa.EmAndamento:
                    Iniciar();
                    break;
                case StatusTarefa.Concluido:
                    Concluir();
                    break;
                case StatusTarefa.Pendente:
                    Reabrir();
                    break;
            }
        }

        public void SoftDelete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DataExclusao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
