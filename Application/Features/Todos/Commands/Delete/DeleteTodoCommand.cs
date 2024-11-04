using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Commands.Delete;

public class DeleteTodoCommand:IRequest<DeletedTodoResponse>
{
    public Guid Id { get; set; }
    
    public class DeleteTodoCommandHandler:IRequestHandler<DeleteTodoCommand,DeletedTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private IMapper _mapper;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<DeletedTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            Todo? todo = await _todoRepository.GetAsync(filter: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _todoRepository.DeleteAsync(todo);

            DeletedTodoResponse deletedTodoResponse = _mapper.Map<DeletedTodoResponse>(todo);
            return deletedTodoResponse;
        }
    }
}