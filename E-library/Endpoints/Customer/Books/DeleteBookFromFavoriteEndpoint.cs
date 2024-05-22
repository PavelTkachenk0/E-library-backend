using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class DeleteBookFromFavoriteEndpoint(BookService bookService) : EndpointWithoutRequest
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Delete("favorites/{id:int}");
        Group<BookGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = await _bookService.DeleteBookFromFavorite(bookId, HttpContext, ct);

        if (result)
        {
            await SendOkAsync(ct);
            return;
        }
        else
        {
            await SendNotFoundAsync(ct); 
            return;
        }
    }
}
