using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Aplication.DTOs;
using Todo.Aplication.Interfaces;
using Todo.Domain.Enums;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TarefaDto>> Create([FromBody] CreateTarefaDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TarefaDto>> GetById(Guid id)
        {
            var tarefa = await _service.GetByIdAsync(id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        // Filtro (Status + Data de Vencimento) ===
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetFiltered(
            [FromQuery] StatusTarefa? status,
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim)
        {
            var tarefas = await _service.GetFilteredAsync(status, dataInicio, dataFim);
            return Ok(tarefas);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTarefaDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/iniciar")]
        public async Task<IActionResult> Iniciar(Guid id)
        {
            await _service.IniciarAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/concluir")]
        public async Task<IActionResult> Concluir(Guid id)
        {
            await _service.ConcluirAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/reabrir")]
        public async Task<IActionResult> Reabrir(Guid id)
        {
            await _service.ReabrirAsync(id);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

