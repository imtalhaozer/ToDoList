using Application.Features.Todos.Queries.GetByStatus;
using Application.Services.Repositories;
using AutoMapper;
using Core.Paging;
using Core.Requests;
using Domain.Entities;
using MediatR;

public class GetTodosByPriorityQuery : IRequest<List<GetTodosByPriorityResponse>>
{
    public string Status { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetTodosByStatusQueryHandler : IRequestHandler<GetTodosByPriorityQuery, List<GetTodosByPriorityResponse>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodosByStatusQueryHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetTodosByPriorityResponse>> Handle(GetTodosByPriorityQuery request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse(request.Status, out Domain.Enums.Priority priority))
            {
                throw new ArgumentException("Invalid priority status value");
            }
            
            Paginate<Todo> todosPaginated = await _todoRepository.GetListAsync(
                predicate: t => t.Priority == priority, 
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted: true 
            );
            
            List<GetTodosByPriorityResponse> response = _mapper.Map<List<GetTodosByPriorityResponse>>(todosPaginated.Items);
            return response;
        }
    }
}