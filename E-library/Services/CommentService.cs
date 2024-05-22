using E_library.DAL;
using E_library.Domain.Models.DTOs;
using E_library.Responses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_library.Services;

public class CommentService(AppDbContext appDbContext)
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddComment(ShortCommentDTO req, int bookId, HttpContext httpContext, CancellationToken ct)
    {
        _appDbContext.Comments.Add(new Domain.Models.Entities.Comment
        {
            Title = req.Title,
            Text = req.Text,
            Score = req.Score,
            BookId = bookId,
            Created = DateTime.UtcNow,
            UserId = _appDbContext.Users.Where(x => x.Email.Contains(httpContext.User.FindFirstValue(ClaimTypes.Email)!)).Select(x => x.Id).Single()
        });

        await _appDbContext.SaveChangesAsync(ct);

        await  Task.CompletedTask;
    }

    public async Task<GetCommentsResponse> GetComments(CancellationToken ct)
    {
        var result = await _appDbContext.Comments.Select(c => new CommentDTO
        {
            Created = c.Created,
            Id = c.Id,
            Score = c.Score,
            Text = c.Text,
            Title = c.Title
        }).ToArrayAsync(ct);

        return new GetCommentsResponse
        {
            Res = result
        };
    }

    public async Task<GetCommentResponse?> GetCommentById(int commentId, CancellationToken ct)
    {
        var result = await _appDbContext.Comments
                        .Where(c => c.Id == commentId)
                        .Select(c => new CommentDTO
                        {
                            Created = c.Created,
                            Id = c.Id,
                            Score = c.Score,
                            Text = c.Text,
                            Title = c.Title
                        }).SingleOrDefaultAsync(ct);

        if (result == null)
        {
            return null;
        }

        return new GetCommentResponse
        {
            Res = result
        };
    }

    public async Task<int?> DeleteComment(int commentId, CancellationToken ct)
    {
        var comment = await _appDbContext.Comments.Where(c => c.Id == commentId).SingleOrDefaultAsync(ct);

        if (comment == null)
        {
            return null;
        }

        _appDbContext.Comments.Remove(comment);

        await _appDbContext.SaveChangesAsync(ct);

        return comment.Id;
    }

    public async Task<GetUserCommentsResponse> GetUserComments(HttpContext httpContext, CancellationToken ct)
    {
        var userId = await _appDbContext.Users
                       .Where(x => x.Email == httpContext.User
                       .FindFirstValue(ClaimTypes.Email))
                       .Select(x => x.Id)
                       .SingleOrDefaultAsync(ct);

        var result = await _appDbContext.Comments.Where(c => c.UserId == userId).Select(c => new CommentDTO
        {
            Author = c.User!.Name,
            Created = c.Created,
            Id = c.Id,
            Score = c.Score,
            Text = c.Text,
            Title = c.Title
        }).ToArrayAsync(ct);

        return new GetUserCommentsResponse
        {
            Res = result
        };
    }
}
