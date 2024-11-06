using Application.Services.Repositories;
using AutoMapper;
using Core.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<ReturnModel<DeletedCategoryResponse>>
{
    public int Id { get; set; }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ReturnModel<DeletedCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ReturnModel<DeletedCategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(filter: b => b.Id == request.Id, cancellationToken: cancellationToken);
            if (category == null)
            {
                return new ReturnModel<DeletedCategoryResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Category not found",
                    StatusCode = 404
                };
            }
            await _categoryRepository.DeleteAsync(category);
            DeletedCategoryResponse response = _mapper.Map<DeletedCategoryResponse>(category);

            return new ReturnModel<DeletedCategoryResponse>
            {
                Data = response,
                Success = true,
                Message = "Category deleted successfully",
                StatusCode = 200
            };
        }
    }
}