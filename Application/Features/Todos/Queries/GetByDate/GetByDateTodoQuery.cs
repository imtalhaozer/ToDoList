using Application.Features.Todos.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Paging;
using Core.Requests;
using Core.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Queries.GetListByDate
{
    public class GetListByDateTodoQuery : IRequest<GetListResponse<GetListTodoListDto>>
    {
        public DateTime Date { get; set; }  
        public PageRequest PageRequest { get; set; } 

        public class GetListByDateTodoQueryHandler : IRequestHandler<GetListByDateTodoQuery, GetListResponse<GetListTodoListDto>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public GetListByDateTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListTodoListDto>> Handle(GetListByDateTodoQuery request, CancellationToken cancellationToken)
            {
                var todos = await _todoRepository.GetListAsync(
                    predicate: todo => todo.StartDate <= request.Date && todo.EndDate >= request.Date && !todo.Completed,
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    withDeleted: true
                );
                
                var response = _mapper.Map<GetListResponse<GetListTodoListDto>>(todos);
                return response;
            }
        }
    }
}