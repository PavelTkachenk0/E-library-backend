namespace E_library.Requests;

public class RegisterRequest : LoginRequest
{
    public string Name { get; set; } = null!;
}
