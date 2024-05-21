using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Comments;

public class DeleteCommentEndpoint(CommentService commentService) : EndpointWithoutRequest<int?>
{
    private readonly CommentService _commentService = commentService;

    public override void Configure()
    {
        Delete("{id:int}");
        Group<CommentGroup>();
    }

    public override async Task<int?> ExecuteAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var result = await _commentService.DeleteComment(id, ct);

        if (result == null)
        {
            return null;
        }
        else
        {
            return result;
        }
    }
}
