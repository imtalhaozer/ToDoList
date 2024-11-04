using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Todos.Queries.GetById;

public class GetByIdTodoResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public User User{ get; set; }
    public Category Category { get; set; }
}