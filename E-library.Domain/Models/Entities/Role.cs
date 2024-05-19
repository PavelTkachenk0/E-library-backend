namespace E_library.Domain.Models.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();
}
