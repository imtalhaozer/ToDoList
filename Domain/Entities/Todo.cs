using Core.Models;
using Domain.Enums;

namespace Domain.Entities;

public class Todo : Entity<Guid>
{
    public Todo()
    {

    }

    public Todo(string title, string description, DateTime startDate, DateTime endDate, Priority priority, int categoryId, bool completed, Category category, string userId, User user)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Priority = priority;
        CategoryId = categoryId;
        Completed = completed;
        Category = category;
        UserId = userId;
        User = user;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public int CategoryId { get; set; }
    public bool Completed { get; set; }
    public Category Category { get; set; }
    public string UserId { get; set; }
    public User User{ get; set; }
}