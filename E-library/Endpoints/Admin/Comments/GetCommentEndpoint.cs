using E_library.Endpoints.Groups.AdminSubGroup;
using E_library.Responses;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Admin.Comments;

public class GetCommentEndpoint(CommentService commentService) : EndpointWithoutRequest<GetCommentResponse?>
{
    private readonly CommentService _commentService = commentService;

    public override void Configure()
    {
        Get("{id:int}");
        Group<CommentGroup>();
    }

    public override async Task<GetCommentResponse?> ExecuteAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var result = await _commentService.GetCommentById(id, ct);

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
