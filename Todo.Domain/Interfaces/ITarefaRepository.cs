using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Domain.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<Tarefa?> GetByIdAsync(Guid id);
        Task<IEnumerable<Tarefa>> GetAllAsync();

        Task<IEnumerable<Tarefa>> GetFilteredAsync(StatusTarefa? status, DateTime? dataInicio, DateTime? dataFim);

        Task AddAsync(Tarefa entity);
        void Update(Tarefa entity);
        void SoftDelete(Tarefa entity);
    }

    // Interface genérica base
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
    }
}
