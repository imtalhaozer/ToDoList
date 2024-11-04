using Core.Models;

namespace Domain.Entities;

public class Category : Entity<int>
{
    public Category()
    {
        
    }

    public Category(string name, List<Todo> toDos)
    {
        Name = name;
        ToDos = toDos;
    }
    public string Name { get; set; }
    public List<Todo> ToDos { get; set; }
}