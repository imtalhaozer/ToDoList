using Application.Services.Repositories;
using AutoMapper;
using Core.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryQuery : IRequest<ReturnModel<GetByIdCategoryResponse>>
{
    public int Id { get; set; }

    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, ReturnModel<GetByIdCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ReturnModel<GetByIdCategoryResponse>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(filter: b => b.Id == request.Id, cancellationToken: cancellationToken);
            if (category == null)
            {
                return new ReturnModel<GetByIdCategoryResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Category not found",
                    StatusCode = 404
                };
            }

            GetByIdCategoryResponse response = _mapper.Map<GetByIdCategoryResponse>(category);
            return new ReturnModel<GetByIdCategoryResponse>
            {
                Data = response,
                Success = true,
                Message = "Category retrieved successfully",
                StatusCode = 200
            };
        }
    }
}