using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class PostBookToFavoriteEndpoint(BookService bookService) : EndpointWithoutRequest
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Post("{id:int}/add-to-favorites");
        Group<BookGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = await _bookService.AddBookToFavorite(bookId, HttpContext, ct);

        if (result)
        {
            await SendOkAsync(ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}
