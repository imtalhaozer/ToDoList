using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand:IRequest<DeletedCategoryResponse>
{
    public int Id { get; set; }
    public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommand,DeletedCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<DeletedCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(filter:b=>b.Id==request.Id, cancellationToken:cancellationToken);
            await _categoryRepository.DeleteAsync(category);
            
            DeletedCategoryResponse response = _mapper.Map<DeletedCategoryResponse>(category);
            return response;
        }
    }
} 