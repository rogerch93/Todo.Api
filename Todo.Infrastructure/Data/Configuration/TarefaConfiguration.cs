using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Data.Configuration
{
    public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Descricao)
                   .HasMaxLength(1000);

            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(t => t.DataCriacao)
                   .IsRequired();

            builder.Property(t => t.DataAtualizacao);

            builder.Property(t => t.DataConclusao);

            builder.Property(t => t.DataVencimento);

            builder.Property(t => t.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(t => t.DataExclusao);

        }
    }
}
