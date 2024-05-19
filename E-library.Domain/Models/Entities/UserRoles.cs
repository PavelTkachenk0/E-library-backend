namespace E_library.Domain.Models.Entities;

public class UserRoles
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public Role? Role { get; set; }
    public User? User { get; set; }
}
