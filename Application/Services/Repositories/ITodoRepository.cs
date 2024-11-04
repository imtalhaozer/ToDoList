using Core.Repository;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ITodoRepository:IAsyncRepository<Todo,Guid>
{
    
}