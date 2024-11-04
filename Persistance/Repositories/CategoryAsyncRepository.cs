using Application.Services.Repositories;
using Core.Repository;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class CategoryAsyncRepository:EfAsyncRepositoryBase<BaseDbContext,Category,int>,ICategoryRepository
{
    public CategoryAsyncRepository(BaseDbContext context) : base(context)
    {
    }
}