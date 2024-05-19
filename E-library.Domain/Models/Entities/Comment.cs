namespace E_library.Domain.Models.Entities;

public class Comment
{
    public int Id { get; set; }
    public string? Title { get; set; } 
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; } 
    public DateTime Created { get; set; }
    public Book? Book {  get; set; } 
    public User? User { get; set; }
}