namespace E_library.Domain.Models.Entities;

public class UserBooks
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public Book? Book { get; set; }
    public User? User {  get; set; } 
}
