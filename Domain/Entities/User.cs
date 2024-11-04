using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public List<Todo> ToDo { get; set; }
}