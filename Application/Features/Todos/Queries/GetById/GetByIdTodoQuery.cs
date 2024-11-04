using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Queries.GetById;

public class GetByIdTodoQuery : IRequest<GetByIdTodoResponse>
{
    public Guid Id { get; set; } 

    public class GetByIdTodoQueryHandler : IRequestHandler<GetByIdTodoQuery, GetByIdTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetByIdTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdTodoResponse> Handle(GetByIdTodoQuery request, CancellationToken cancellationToken)
        {
            Todo? todo = await _todoRepository.GetAsync(filter: b => b.Id == request.Id, cancellationToken: cancellationToken);
            
            if (todo == null)
            {
                throw new Exception("Todo not found");
            }
            
            GetByIdTodoResponse response = _mapper.Map<GetByIdTodoResponse>(todo);
            return response;
        }
    }
}