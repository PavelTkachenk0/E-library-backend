namespace E_library.Domain.Models.DTOs;

public class AuthorDTO : ShortAuthorDTO
{
    public ShortBookDTO[]? Books { get; set; } 
}
