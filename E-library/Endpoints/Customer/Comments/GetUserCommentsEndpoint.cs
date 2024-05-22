using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Comments;

public class GetUserCommentsEndpoint(CommentService commentService) : EndpointWithoutRequest<GetUserCommentsResponse>
{
    private readonly CommentService _commentService = commentService;

    public override void Configure()
    {
        Get("all-for-user");
        Group<CommentGroup>();
    }

    public override async Task<GetUserCommentsResponse> ExecuteAsync(CancellationToken ct)
    {
        var result = await _commentService.GetUserComments(HttpContext ,ct);

        return result;
    }
}
