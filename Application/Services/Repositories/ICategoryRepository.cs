using Core.Repository;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ICategoryRepository:IAsyncRepository<Category,int>
{
    
}