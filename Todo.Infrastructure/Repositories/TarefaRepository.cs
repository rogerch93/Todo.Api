using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa?> GetByIdAsync(Guid id)
              => await _context.Tarefas.FindAsync(id);

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
            => await _context.Tarefas.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Tarefa>> GetFilteredAsync(StatusTarefa? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var query = _context.Tarefas.AsNoTracking();

            // Filtro por Status (se informado)
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            // Filtro por Data de Vencimento (se informado)
            if (dataInicio.HasValue)
            {
                query = query.Where(t => t.DataVencimento >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                query = query.Where(t => t.DataVencimento <= dataFim.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Tarefa entity)
            => await _context.Tarefas.AddAsync(entity);

        public void Update(Tarefa entity)
            => _context.Tarefas.Update(entity);

        public void SoftDelete(Tarefa entity)
        {
            entity.SoftDelete();
            _context.Tarefas.Update(entity);
        }


    }
}
