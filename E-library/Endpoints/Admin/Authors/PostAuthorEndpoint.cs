using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Authors;

public class PostAuthorEndpoint(AuthorService authorService) : Endpoint<PostAuthorRequest, PostAuthorResponse?>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Post("");
        Group<AuthorGroup>();
    }

    public override async Task<PostAuthorResponse?> ExecuteAsync(PostAuthorRequest req, CancellationToken ct)
    {
        var result = await _authorService.CreateAuthor(req, ct);

        if (result == null)
        {
            await SendErrorsAsync(cancellation:ct);
        }

        return result;
    }
}
