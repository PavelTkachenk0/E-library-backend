namespace E_library.Domain.Models.Entities;

public class Book
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string CoverPath { get; set; } = null!;
    public string Genre {  get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DateOfPublishing { get; set; } 
    public DateTime? DateOfWriting { get; set; }
    public int CountOfPages { get; set; }
    public Author? Author { get; set; }
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserBooks> UserBooks { get; set; } = new HashSet<UserBooks>();
}

