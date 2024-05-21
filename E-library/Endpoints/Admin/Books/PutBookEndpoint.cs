using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Books;

public class PutBookEndpoint(BookService bookService) : Endpoint<PutBookRequest, PutBookResponse?>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Put("{id:int}");
        Group<BookGroup>();
    }

    public override async Task<PutBookResponse?> ExecuteAsync(PutBookRequest req, CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = await _bookService.UpdateBook(bookId, req, ct);

        if (result != null)
        {
            return result;
        }
        else
        {
            return null;
        }
    }
}
