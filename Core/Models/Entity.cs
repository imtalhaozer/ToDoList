using Core.Repository;

namespace Core.Models;

public class Entity<TId>:IEntityTimestamps
{
    public Entity()
    {
        Id = default;
        CreatedDate = DateTime.Now;
    }

    public Entity(TId id)
    {
        Id=id;
    }
    public TId Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}