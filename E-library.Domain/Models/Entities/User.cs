using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_library.Domain.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    public ICollection<UserBooks> UserBooks { get; set; } = new HashSet<UserBooks>();
    public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    public ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();
}
