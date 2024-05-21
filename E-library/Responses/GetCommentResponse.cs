using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetCommentResponse
{
    public CommentDTO Res { get; set; } = null!;
}
