using FluentAssertions;
using Moq;
using Todo.Aplication.DTOs;
using Todo.Aplication.Services;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Persistence;
using Xunit;

namespace Todo.Tests.Test
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly TarefaService _service;

        public TarefaServiceTests()
        {
            _repositoryMock = new Mock<ITarefaRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _service = new TarefaService(
                _repositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTaskAndReturnDto_WhenValidData()
        {
            // Arrange
            var createDto = new CreateTarefaDto
            {
                Titulo = "Desenvolver testes unitários",
                Descricao = "Testando criação de tarefa",
                DataVencimento = DateTime.UtcNow.AddDays(7)
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Tarefa>()))
                           .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.CommitAsync())
                           .ReturnsAsync(1);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Titulo.Should().Be(createDto.Titulo);
            result.Status.Should().Be(StatusTarefa.Pendente);
            result.DataVencimento.Should().BeCloseTo(createDto.DataVencimento!.Value, TimeSpan.FromSeconds(2));

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Tarefa>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ConcluirAsync_ShouldMarkTaskAsConcluido_WhenTaskExists()
        {
            // Arrange
            var tarefaId = Guid.NewGuid();
            var tarefa = new Tarefa("Tarefa para concluir");

            _repositoryMock.Setup(r => r.GetByIdAsync(tarefaId))
                           .ReturnsAsync(tarefa);

            _unitOfWorkMock.Setup(u => u.CommitAsync())
                           .ReturnsAsync(1);

            // Act
            await _service.ConcluirAsync(tarefaId);

            // Assert
            tarefa.Status.Should().Be(StatusTarefa.Concluido);
            tarefa.DataConclusao.Should().NotBeNull();

            _repositoryMock.Verify(r => r.Update(It.IsAny<Tarefa>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenTaskNotFound()
        {
            // Arrange
            var tarefaId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetByIdAsync(tarefaId))
                           .ReturnsAsync((Tarefa?)null);

            // Act
            var result = await _service.GetByIdAsync(tarefaId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateTarefaDto
            {
                Titulo = "Título Atualizado"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((Tarefa?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _service.UpdateAsync(Guid.NewGuid(), updateDto));
        }
    }
}