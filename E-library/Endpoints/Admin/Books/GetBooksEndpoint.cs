using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Books;

public class GetBooksEndpoint(BookService bookService) : EndpointWithoutRequest<GetAdminBooksResponse>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Get("");
        Group<BookGroup>();
    }

    public override Task<GetAdminBooksResponse> ExecuteAsync(CancellationToken ct)
    {
        var result = _bookService.GetAdminBooks(ct);

        return result;
    }
}
