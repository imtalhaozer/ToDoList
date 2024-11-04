using Application.Services.Repositories;
using Core.Repository;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class TodoRepository:EfAsyncRepositoryBase<BaseDbContext,Todo,Guid>,ITodoRepository
{
    public TodoRepository(BaseDbContext context) : base(context)
    {
    }
}