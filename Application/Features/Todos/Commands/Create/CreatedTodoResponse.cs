using Domain.Entities;
using Domain.Enums;

namespace Application.Features.ToDos.Commands.Create;

public class CreatedTodoResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public User User{ get; set; }
    public Category Category { get; set; }
}