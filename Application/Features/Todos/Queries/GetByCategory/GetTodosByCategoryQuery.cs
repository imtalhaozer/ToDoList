using Application.Features.Todos.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Paging;
using Core.Requests;
using Core.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Queries.GetListByCategory
{
    public class GetListByCategoryTodoQuery : IRequest<GetListResponse<GetListTodoListDto>>
    {
        public List<int> CategoryIds { get; set; }  
        public PageRequest PageRequest { get; set; }  

        public class GetListByCategoryTodoQueryHandler : IRequestHandler<GetListByCategoryTodoQuery, GetListResponse<GetListTodoListDto>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public GetListByCategoryTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListTodoListDto>> Handle(GetListByCategoryTodoQuery request, CancellationToken cancellationToken)
            {
                var todos = await _todoRepository.GetListAsync(
                    predicate: todo => request.CategoryIds.Contains(todo.CategoryId) && !todo.Completed,
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