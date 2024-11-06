using Moq;
using NUnit.Framework;
using AutoMapper;
using Application.Features.ToDos.Commands.Create;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.Features.ToDos.Commands.Create.CreateTodoCommand;

namespace Application.Tests.Features.ToDos.Commands.Create
{
    [TestFixture]
    public class CreateTodoCommandHandlerTests
    {
        private Mock<ITodoRepository> _mockTodoRepository;
        private Mock<IMapper> _mockMapper;
        private CreateTodoCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();

            _handler = new CreateTodoCommandHandler(_mockTodoRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnCreatedTodoResponse()
        {
            var command = new CreateTodoCommand
            {
                Title = "Test Todo",
                Description = "Test Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Priority = Priority.High,
                UserId = "testUserId",
                CategoryId = 1
            };

            var todo = new Todo
            {
                Id = new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
                Title = command.Title,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Priority = command.Priority,
                UserId = command.UserId,
                CategoryId = command.CategoryId
            };

            var createdTodoResponse = new CreatedTodoResponse
            {
                Title = todo.Title,
                Description = todo.Description
            };
            _mockMapper.Setup(m => m.Map<Todo>(It.IsAny<CreateTodoCommand>())).Returns(todo);
            _mockTodoRepository.Setup(repo => repo.AddAsync(It.IsAny<Todo>())).ReturnsAsync(todo);
            _mockMapper.Setup(m => m.Map<CreatedTodoResponse>(It.IsAny<Todo>())).Returns(createdTodoResponse);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(todo.Title, result.Title);
            Assert.AreEqual(todo.Description, result.Description);

            _mockTodoRepository.Verify(repo => repo.AddAsync(It.IsAny<Todo>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Todo>(It.IsAny<CreateTodoCommand>()), Times.Once);
            _mockMapper.Verify(m => m.Map<CreatedTodoResponse>(It.IsAny<Todo>()), Times.Once);
        }
    }
}
