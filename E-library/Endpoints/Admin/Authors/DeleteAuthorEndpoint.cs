using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Authors;

public class DeleteAuthorEndpoint(AuthorService authorService) : EndpointWithoutRequest<int?>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Delete("{id:int}");
        Group<AuthorGroup>();
    }

    public override async Task<int?> ExecuteAsync(CancellationToken ct)
    {
        var authorId = Route<int>("id");

        var result = await _authorService.DeleteAuthor(authorId, ct);

        if (result == null)
        {
            await SendNotFoundAsync(ct);
            return null;
        }

        return result;
    }
}
