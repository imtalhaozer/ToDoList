using Application.Services.Repositories;
using AutoMapper;
using Core.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<ReturnModel<UpdatedCategoryResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ReturnModel<UpdatedCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ReturnModel<UpdatedCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(filter: b => b.Id == request.Id, cancellationToken: cancellationToken);
            if (category == null)
            {
                return new ReturnModel<UpdatedCategoryResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Category not found",
                    StatusCode = 404
                };
            }
            
            category = _mapper.Map(request, category);
            await _categoryRepository.UpdateAsync(category);
            
            UpdatedCategoryResponse updatedCategoryResponse = _mapper.Map<UpdatedCategoryResponse>(category);

            return new ReturnModel<UpdatedCategoryResponse>
            {
                Data = updatedCategoryResponse,
                Success = true,
                Message = "Category updated successfully",
                StatusCode = 200
            };
        }
    }
}
