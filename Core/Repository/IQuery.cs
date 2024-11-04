namespace Core.Repository;

public interface IQuery<TEntity>
{
    IQueryable<TEntity> Query();
}