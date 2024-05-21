using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Books;

public class DeleteBookEndpoint(BookService bookService) : EndpointWithoutRequest<int?>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Delete("{id:int}");
        Group<BookGroup>();
    }

    public override async Task<int?> ExecuteAsync(CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = await _bookService.DeleteBook(bookId, ct);

        if (result == null)
        {
            await SendNotFoundAsync(ct);
            return null;
        }

        return result;
    }
}
