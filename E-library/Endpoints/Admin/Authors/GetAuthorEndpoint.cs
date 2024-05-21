using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Authors;

public class GetAuthorEndpoint(AuthorService authorService) : EndpointWithoutRequest<GetAdminAuthorResponse?>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Get("{id:int}");
        Group<AuthorGroup>();
    }

    public override async Task<GetAdminAuthorResponse?> ExecuteAsync(CancellationToken ct)
    {
        var authorId = Route<int>("id");

        var result = await _authorService.GetAdminAuthor(authorId, ct);

        if (result == null)
        {
            await SendNotFoundAsync(ct);
            return null;
        }
        else
        {
            return result;
        }
    }
}
