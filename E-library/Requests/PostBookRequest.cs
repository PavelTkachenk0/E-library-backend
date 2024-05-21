namespace E_library.Requests;

public class PostBookRequest
{
    public string Name { get; set; } = null!;
    public int AuthorId { get; set; } 
    public string? Description { get; set; }
    public string CoverPath { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public DateTime DateOfPublishing { get; set; }
    public DateTime DateOfWriting { get; set; }
    public int CountOfPages { get; set; }
}
