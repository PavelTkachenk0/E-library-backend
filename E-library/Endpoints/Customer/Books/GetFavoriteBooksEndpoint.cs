using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class GetFavoriteBooksEndpoint(BookService bookService) : EndpointWithoutRequest<GetFavoriteBooksResponse>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Get("favorites");
        Group<BookGroup>();
    }

    public override Task<GetFavoriteBooksResponse> ExecuteAsync(CancellationToken ct)
    {
        var result = _bookService.GetFavoriteBooks(HttpContext, ct);

        return result;
    }
}
