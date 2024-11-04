using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Todos.Commands.Update;

public class UpdateTodoCommand:IRequest<UpdatedTodoResponse>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public string UserId { get; set; }
    public int CategoryId { get; set; }
    
    public class UpdateTodoCommandHandler:IRequestHandler<UpdateTodoCommand,UpdatedTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public UpdateTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedTodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            Todo? todo = await _todoRepository.GetAsync(filter:b=>b.Id==request.Id, cancellationToken:cancellationToken);
            todo = _mapper.Map(request,todo);
            await _todoRepository.UpdateAsync(todo);
            UpdatedTodoResponse updatedTodoResponse = _mapper.Map<UpdatedTodoResponse>(todo);
            return updatedTodoResponse;
        }
    }
}