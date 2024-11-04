using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Todos.Commands.Update;

public class UpdatedTodoResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public User User{ get; set; }
    public Category Category { get; set; }
}