using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class GetSearchedBookEndpoint(BookService bookService) : Endpoint<Pattern, GetSearchedBookResponse>
{
    private readonly BookService _bookService = bookService;
    public override void Configure()
    {
        Get("");
        Group<BookGroup>();
    }

    public override Task<GetSearchedBookResponse> ExecuteAsync(Pattern req, CancellationToken ct)
    {
        var result = _bookService.GetSearchedBook(req.SearchedPattern, ct);

        return result;
    }
}
