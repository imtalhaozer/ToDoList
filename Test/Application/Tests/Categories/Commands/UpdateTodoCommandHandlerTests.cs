using Moq;
using NUnit.Framework;
using AutoMapper;
using Application.Features.Todos.Commands.Update;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.Features.Todos.Commands.Update.UpdateTodoCommand;

namespace Application.Tests.Features.Todos.Commands.Update
{
    [TestFixture]
    public class UpdateTodoCommandHandlerTests
    {
        private Mock<ITodoRepository> _mockTodoRepository;
        private Mock<IMapper> _mockMapper;
        private UpdateTodoCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateTodoCommandHandler(_mockTodoRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnUpdatedTodoResponse()
        {
            var command = new UpdateTodoCommand
            {
                Id = Guid.NewGuid(),
                Title = "Updated Title",
                Description = "Updated Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Priority = Priority.High,
                UserId = "UserId",
                CategoryId = 1
            };

            var existingTodo = new Todo
            {
                Id = command.Id,
                Title = "Old Title",
                Description = "Old Description",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now,
                Priority = Priority.Low,
                UserId = command.UserId,
                CategoryId = command.CategoryId
            };

            var updatedTodoResponse = new UpdatedTodoResponse
            {
                Id = command.Id,
                Title = command.Title,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Priority = command.Priority
            };

            _mockTodoRepository.Setup(repo => repo.GetAsync(It.IsAny<Func<Todo, bool>>(), CancellationToken.None)).ReturnsAsync(existingTodo);
            _mockMapper.Setup(m => m.Map(It.IsAny<UpdateTodoCommand>(), It.IsAny<Todo>())).Returns(existingTodo);
            _mockTodoRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Todo>())).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<UpdatedTodoResponse>(It.IsAny<Todo>())).Returns(updatedTodoResponse);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(command.Id, result.Id);
            Assert.AreEqual(command.Title, result.Title);
            Assert.AreEqual(command.Description, result.Description);
            Assert.AreEqual(command.StartDate, result.StartDate);
            Assert.AreEqual(command.EndDate, result.EndDate);
            Assert.AreEqual(command.Priority, result.Priority);

            _mockTodoRepository.Verify(repo => repo.GetAsync(It.IsAny<Func<Todo, bool>>(), CancellationToken.None), Times.Once);
            _mockTodoRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Todo>()), Times.Once);
            _mockMapper.Verify(m => m.Map<UpdatedTodoResponse>(It.IsAny<Todo>()), Times.Once);
        }
    }
}
