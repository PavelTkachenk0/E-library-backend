namespace E_library.Domain.Models.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhotoPath { get; set; } = null!;
    public string? Genre { get; set; } = null!;
    public string? ShortBio {  get; set; } 
    public ICollection<Book> Books { get; set;} = new HashSet<Book>();
}