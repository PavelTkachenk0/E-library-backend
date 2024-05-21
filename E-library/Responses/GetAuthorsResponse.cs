using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetAuthorsResponse
{
    public ShortAuthorDTO[]? Res {  get; set; }
}
