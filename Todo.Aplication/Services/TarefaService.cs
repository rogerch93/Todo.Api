using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Aplication.DTOs;
using Todo.Aplication.Interfaces;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Persistence;

namespace Todo.Aplication.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TarefaService(ITarefaRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TarefaDto> CreateAsync(CreateTarefaDto dto)
        {
            var tarefa = new Tarefa(dto.Titulo, dto.Descricao, dto.DataVencimento);
            await _repository.AddAsync(tarefa);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task<TarefaDto?> GetByIdAsync(Guid id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            return _mapper.Map<TarefaDto?>(tarefa);
        }

        public async Task<IEnumerable<TarefaDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<TarefaDto>>(await _repository.GetAllAsync());

        public async Task<IEnumerable<TarefaDto>> GetFilteredAsync(StatusTarefa? status, DateTime? dataInicio, DateTime? dataFim)
            => _mapper.Map<IEnumerable<TarefaDto>>(
                await _repository.GetFilteredAsync(status, dataInicio, dataFim));

        public async Task UpdateAsync(Guid id, UpdateTarefaDto dto)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            tarefa.Atualizar(dto.Titulo, dto.Descricao, dto.DataVencimento);

            if (dto.Status.HasValue)
                tarefa.AlterarStatus(dto.Status.Value);

            _repository.Update(tarefa);
            await _unitOfWork.CommitAsync();
        }

        public async Task IniciarAsync(Guid id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada.");
            tarefa.Iniciar();
            _repository.Update(tarefa);
            await _unitOfWork.CommitAsync();
        }

        public async Task ConcluirAsync(Guid id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada.");
            tarefa.Concluir();
            _repository.Update(tarefa);
            await _unitOfWork.CommitAsync();
        }

        public async Task ReabrirAsync(Guid id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada.");
            tarefa.Reabrir();
            _repository.Update(tarefa);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada.");

            tarefa.SoftDelete();
            _repository.Update(tarefa);
            await _unitOfWork.CommitAsync();
        }
    }
}
