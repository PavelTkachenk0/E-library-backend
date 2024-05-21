using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetBookCommentsResponse
{
    public CommentDTO[]? Res { get; set; }
}
