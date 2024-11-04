using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.ToDos.Commands.Create;

public class CreateTodoCommand:IRequest<CreatedTodoResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public string UserId { get; set; }
    public int CategoryId { get; set; }
    
    public class CreateTodoCommandHandler:IRequestHandler<CreateTodoCommand,CreatedTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        public async Task<CreatedTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            Todo todo = _mapper.Map<Todo>(request);
            await _todoRepository.AddAsync(todo);
            CreatedTodoResponse createdTodoResponse = _mapper.Map<CreatedTodoResponse>(todo);
            return createdTodoResponse;
        }
    }
}