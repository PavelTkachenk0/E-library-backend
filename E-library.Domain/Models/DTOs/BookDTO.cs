namespace E_library.Domain.Models.DTOs;

public class BookDTO : ShortBookDTO
{
    public int AuthorId { get; set; }
    public string? Description { get; set; }
    public int CountOfPages { get; set; }
}
