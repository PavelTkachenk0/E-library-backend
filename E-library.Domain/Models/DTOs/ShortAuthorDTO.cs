namespace E_library.Domain.Models.DTOs;

public class ShortAuthorDTO : PostAuthorDTO
{
    public int Id { get; set; }
}

public class ShortAuthorDTOWithBook : ShortAuthorDTO
{
    public AuthorBookDTO[]? Books { get; set; }
}

public class AuthorBookDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public double TotalScore { get; set; }
}

public abstract class PostAuthorDTO
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhotoPath { get; set; } = null!;
    public string? Genre { get; set; }
    public string? ShortBio { get; set; }
}