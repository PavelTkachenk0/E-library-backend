using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class GetBookCommentsEndpoint(BookService bookService) : EndpointWithoutRequest<GetBookCommentsResponse>
{
    private readonly BookService _bookService = bookService;

    public override void Configure()
    {
        Get("{id:int}/comments");
        Group<BookGroup>();
    }

    public override Task<GetBookCommentsResponse> ExecuteAsync(CancellationToken ct)
    {
        var bookId = Route<int>("id");

        var result = _bookService.GetBookComments(bookId, ct);

        return result;
    }
}
