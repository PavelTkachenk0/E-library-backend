using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class GetBookEndpoint(BookService bookService) : EndpointWithoutRequest<GetBookResponse>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Get("{id:int}");
        Group<BookGroup>();
    }

    public override async Task<GetBookResponse> ExecuteAsync(CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = await _bookService.GetBook(bookId, ct);

        return result;
    }
}
