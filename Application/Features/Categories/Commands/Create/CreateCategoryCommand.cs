using Application.Services.Repositories;
using AutoMapper;
using Core.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<ReturnModel<CreatedCategoryResponse>>
{
    public string Name { get; set; }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ReturnModel<CreatedCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ReturnModel<CreatedCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(request);
            await _categoryRepository.AddAsync(category);
            CreatedCategoryResponse createdCategoryResponse = _mapper.Map<CreatedCategoryResponse>(category);

            return new ReturnModel<CreatedCategoryResponse>
            {
                Data = createdCategoryResponse,
                Success = true,
                Message = "Category created successfully",
                StatusCode = 201
            };
        }
    }
}