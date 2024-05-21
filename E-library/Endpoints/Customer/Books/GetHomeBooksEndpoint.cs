using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class GetHomeBooksEndpoint(BookService bookService) : EndpointWithoutRequest<GetHomeBooksResposne>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Get("home");
        Group<BookGroup>();
    }

    public override Task<GetHomeBooksResposne> ExecuteAsync(CancellationToken ct)
    {
        var result = _bookService.GetHomeBooks(ct);

        return result;
    }
}
