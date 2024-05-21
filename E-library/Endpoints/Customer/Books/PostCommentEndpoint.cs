using E_library.Domain.Models.DTOs;
using E_library.Endpoints.Groups.CustomerSubGroup;
using E_library.Services;
using FastEndpoints;

namespace E_library.Endpoints.Customer.Books;

public class PostCommentEndpoint(CommentService commentService) : Endpoint<ShortCommentDTO>
{
    private readonly CommentService _commentService = commentService;

    public override void Configure()
    {
        Post("{id:int}/add-comment");
        Group<BookGroup>();
    }

    public override async Task HandleAsync(ShortCommentDTO req, CancellationToken ct)
    {
        var bookId = Route<int>("id");

        await _commentService.AddComment(req, bookId, HttpContext, ct);

        await SendOkAsync(ct);
    }
}
