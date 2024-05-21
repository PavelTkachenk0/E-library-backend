using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Authors;

public class GetAuthorEndpoint(AuthorService authorService) : EndpointWithoutRequest<GetAuthorResponse?>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Get("{id:int}");
        Group<AuthorGroup>();
    }

    public override async Task<GetAuthorResponse?> ExecuteAsync(CancellationToken ct)
    {
        var authorId = Route<int>("id");

        var result = await _authorService.GetAuthor(authorId, ct);

        if (result == null)
        {
            await SendNotFoundAsync(ct);
            return null;
        }

        return result;
    }
}
