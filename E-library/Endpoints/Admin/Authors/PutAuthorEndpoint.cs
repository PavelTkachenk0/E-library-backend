using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Authors;

public class PutAuthorEndpoint(AuthorService authorService) : Endpoint<PutAuthorRequest, PutAuthorResponse?>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Put("{id:int}");
        Group<AuthorGroup>();
    }

    public override async Task<PutAuthorResponse?> ExecuteAsync(PutAuthorRequest req, CancellationToken ct)
    {
        var authorId = Route<int>("id");

        var result = await _authorService.UpdateAuthor(authorId, req, ct);

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
