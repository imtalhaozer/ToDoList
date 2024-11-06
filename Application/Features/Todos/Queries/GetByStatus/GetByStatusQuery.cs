using System.Linq.Expressions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Queries.GetByStatus
{
    public class GetByStatusTodoQuery : IRequest<List<GetByStatusTodoResponse>>
    {
        public bool IsPastDue { get; set; }

        public class GetByStatusTodoQueryHandler : IRequestHandler<GetByStatusTodoQuery, List<GetByStatusTodoResponse>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public GetByStatusTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<List<GetByStatusTodoResponse>> Handle(GetByStatusTodoQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Todo, bool>> filter = request.IsPastDue 
                    ? (todo => todo.EndDate < DateTime.Now && !todo.Completed)
                    : (todo => todo.EndDate >= DateTime.Now && !todo.Completed);
                
                var todos = await _todoRepository.GetListAsync(predicate: filter, cancellationToken: cancellationToken);
                
                return _mapper.Map<List<GetByStatusTodoResponse>>(todos);
            }
        }
    }
}