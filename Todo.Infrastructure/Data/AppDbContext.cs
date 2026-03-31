using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Infrastructure.Data.Configuration;

namespace Todo.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // Todos os Enums do tipo StatusTarefa serão armazenados como string no banco
            configurationBuilder
                .Properties<StatusTarefa>()
                .HaveConversion<string>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>().HasQueryFilter(t => !t.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
