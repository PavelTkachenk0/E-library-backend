using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetBookResponse
{
    public BookResponse? Res { get; set; } 
}

public class BookResponse : BookDTO
{
    public CommentDTO[]? Comments { get; set; }
}