using Moq;
using NUnit.Framework;
using AutoMapper;
using Application.Features.Todos.Commands.Delete;
using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.Features.Todos.Commands.Delete.DeleteTodoCommand;

namespace Application.Tests.Features.Todos.Commands.Delete
{
    [TestFixture]
    public class DeleteTodoCommandHandlerTests
    {
        private Mock<ITodoRepository> _mockTodoRepository;
        private Mock<IMapper> _mockMapper;
        private DeleteTodoCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DeleteTodoCommandHandler(_mockTodoRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnDeletedTodoResponse()
        {
            var command = new DeleteTodoCommand
            {
                Id = Guid.NewGuid()
            };

            var todo = new Todo
            {
                Id = command.Id,
                Title = "Test Todo",
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            var deletedTodoResponse = new DeletedTodoResponse
            {
                Id = todo.Id,
                Title = todo.Title
            };

            _mockTodoRepository.Setup(repo => repo.GetAsync(It.IsAny<Func<Todo, bool>>(), CancellationToken.None)).ReturnsAsync(todo);
            _mockTodoRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Todo>())).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<DeletedTodoResponse>(It.IsAny<Todo>())).Returns(deletedTodoResponse);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(todo.Id, result.Id);
            Assert.AreEqual(todo.Title, result.Title);

            _mockTodoRepository.Verify(repo => repo.GetAsync(It.IsAny<Func<Todo, bool>>(), CancellationToken.None), Times.Once);
            _mockTodoRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Todo>()), Times.Once);
            _mockMapper.Verify(m => m.Map<DeletedTodoResponse>(It.IsAny<Todo>()), Times.Once);
        }
    }
}
