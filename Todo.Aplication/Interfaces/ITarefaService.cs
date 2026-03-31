using Todo.Aplication.DTOs;
using Todo.Domain.Enums;

namespace Todo.Aplication.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaDto> CreateAsync(CreateTarefaDto dto);
        Task<TarefaDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TarefaDto>> GetAllAsync();

        Task<IEnumerable<TarefaDto>> GetFilteredAsync(StatusTarefa? status, DateTime? dataInicio, DateTime? dataFim);

        Task UpdateAsync(Guid id, UpdateTarefaDto dto);
        Task IniciarAsync(Guid id);
        Task ConcluirAsync(Guid id);
        Task ReabrirAsync(Guid id);
        Task DeleteAsync(Guid id);   // Soft Delete
    }
}