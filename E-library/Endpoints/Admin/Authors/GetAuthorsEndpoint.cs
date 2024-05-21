using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Authors;

public class GetAuthorsEndpoint(AuthorService authorService) : EndpointWithoutRequest<GetAuthorsResponse>
{
    private readonly AuthorService _authorService = authorService;

    public override void Configure()
    {
        Get("");
        Group<AuthorGroup>();
    }

    public override Task<GetAuthorsResponse> ExecuteAsync(CancellationToken ct)
    {
        var result = _authorService.GetAuthors(ct);

        return result;
    }
}
