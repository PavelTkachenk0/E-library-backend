using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class AuthResponse : UserDTO
{
    public string[] Roles { get; set; } = null!;
}
