using E_library.Requests;

namespace E_library.Responses;

public class AuthResponse : RegisterRequest
{
    public string[] Roles { get; set; } = null!;
}
