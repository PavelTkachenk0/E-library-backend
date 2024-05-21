using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Books;

public class PostBookEndpoint(BookService bookService) : Endpoint<PostBookRequest, PostBookResponse?>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Post("");
        Group<BookGroup>();
    }

    public override async Task<PostBookResponse?> ExecuteAsync(PostBookRequest req, CancellationToken ct)
    {
        var result = await _bookService.CreateBook(req, ct);

        if (result == null)
        {
            await SendErrorsAsync(cancellation:ct);
            return null;
        }

        return result;
    }
}
