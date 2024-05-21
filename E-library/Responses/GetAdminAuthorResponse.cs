using E_library.Domain.Models.DTOs;

namespace E_library.Responses;

public class GetAdminAuthorResponse
{
    public ShortAuthorDTOWithBook Res { get; set; } = null!;
}
