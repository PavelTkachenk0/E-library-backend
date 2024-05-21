using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Comments;

public class GetCommentsEndpoint(CommentService commentService) : EndpointWithoutRequest<GetCommentsResponse>
{
    private readonly CommentService _commentService = commentService;

    public override void Configure()
    {
        Get("");
        Group<CommentGroup>();
    }

    public override async Task<GetCommentsResponse> ExecuteAsync(CancellationToken ct)
    {
        var result = await _commentService.GetComments(ct);

        return result;
    }
}
