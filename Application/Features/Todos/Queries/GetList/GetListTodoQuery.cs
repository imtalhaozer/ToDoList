using Application.Services.Repositories;
using AutoMapper;
using Core.Paging;
using Core.Requests;
using Core.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Todos.Queries.GetList;

public class GetListTodoQuery : IRequest<GetListResponse<GetListTodoListDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTodoQueryHandler : IRequestHandler<GetListTodoQuery, GetListResponse<GetListTodoListDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetListTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTodoListDto>> Handle(GetListTodoQuery request, CancellationToken cancellationToken)
        {
            Paginate<Todo> todos = await _todoRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted: true 
            );

            GetListResponse<GetListTodoListDto> response = _mapper.Map<GetListResponse<GetListTodoListDto>>(todos);
            return response;
        }
    }
}