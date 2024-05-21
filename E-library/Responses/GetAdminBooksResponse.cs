using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetAdminBooksResponse
{
    public ShortBookDTO[]? Res { get; set; }
}
