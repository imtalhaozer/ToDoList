using System.Drawing;
using Application.Services.Repositories;
using AutoMapper;
using Core.Paging;
using Core.Requests;
using Core.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery:IRequest<GetListResponse<GetListCategoryListDto>>
{
    public PageRequest PageRequest { get; set; }
    
    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCategoryListDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            Paginate<Category> categories = await _categoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted:true
                );
            GetListResponse<GetListCategoryListDto> response = _mapper.Map<GetListResponse<GetListCategoryListDto>>(categories);
            return response;
        }
    }
}