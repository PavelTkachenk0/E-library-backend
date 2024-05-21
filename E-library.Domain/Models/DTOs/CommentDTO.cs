namespace E_library.Domain.Models.DTOs;

public class CommentDTO : ShortCommentDTO
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public DateTime? Created { get; set; }
}
