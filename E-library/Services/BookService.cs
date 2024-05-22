using AutoMapper;
using E_library.DAL;
using E_library.Domain.Models.DTOs;
using E_library.Domain.Models.Entities;
using E_library.Requests;
using E_library.Responses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_library.Services;

public class BookService(AppDbContext appDbContext, IMapper mapper)
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GetHomeBooksResposne> GetHomeBooks(CancellationToken ct)
    {
        var result = await _appDbContext.Books
            .Select(b => new BookDTO
            {
                Id = b.Id,
                AuthorId = b.AuthorId,
                Name = b.Title,
                CoverPath = b.CoverPath,
                CountOfPages = b.CountOfPages,
                DateOfPublishing = b.DateOfPublishing,
                DateOfWriting = b.DateOfWriting,
                Description = b.Description,
                Genre = b.Genre,
                TotalScore = _appDbContext.Comments.Where(c => c.BookId == b.Id).Count() == 0 ? 0 
                                    : (Math.Truncate(_appDbContext.Comments
                                    .Where(c => c.BookId == b.Id)
                                    .Select(c => c.Score)
                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == b.Id).Count() * 100)) / 100
            }).ToArrayAsync(ct);

        return new GetHomeBooksResposne
        {
            Res = result
        };
    }

    public async Task<GetFavoriteBooksResponse> GetFavoriteBooks(HttpContext httpContext, CancellationToken ct)
    {
        var result = await _appDbContext.Books
                .Where(x => x.Users.Select(u => u.Email).Contains(httpContext.User.FindFirstValue(ClaimTypes.Email)))
                .Select(x => new BookDTO
                {
                    AuthorId = x.AuthorId,
                    CountOfPages = x.CountOfPages,
                    CoverPath = x.CoverPath,
                    DateOfPublishing = x.DateOfPublishing,
                    DateOfWriting = x.DateOfWriting,
                    Description = x.Description,
                    Genre = x.Genre,
                    Name = x.Title,
                    Id = x.Id,
                    TotalScore = _appDbContext.Comments.Where(c => c.BookId == x.Id).Count() == 0 ? 0
                                   : (Math.Truncate(_appDbContext.Comments
                                    .Where(c => c.BookId == x.Id)
                                    .Select(c => c.Score)
                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == x.Id).Count() * 100)) / 100
                }).ToArrayAsync(ct);

        return new GetFavoriteBooksResponse
        {
            Res = result
        };
    }

    public async Task<GetBookResponse> GetBook(int bookId, CancellationToken ct)
    {
        var resut = await _appDbContext.Books
            .Where(x => x.Id ==  bookId)
            .Select(x => new BookResponse
            {
                AuthorId = x.AuthorId,
                CountOfPages = x.CountOfPages,
                CoverPath = x.CoverPath,
                DateOfPublishing = x.DateOfPublishing,
                DateOfWriting = x.DateOfWriting,
                Description = x.Description,
                Genre = x.Genre,
                Name = x.Title,
                Id = x.Id,
                Comments = _appDbContext.Comments
                            .Where(c => c.BookId == bookId)
                            .Select(c => new CommentDTO
                            {
                                Id = c.Id,
                                Created = c.Created,
                                Score = c.Score,
                                Text = c.Text,
                                Title = c.Title,
                                Author = _appDbContext.Users.Where(u => u.Id == c.UserId).Select(u => u.Name).Single()
                            }).Take(3).ToArray(),
                TotalScore = _appDbContext.Comments.Where(c => c.BookId == bookId).Count() == 0 ? 0 :
                                    (Math.Truncate(_appDbContext.Comments
                                    .Where(c => c.BookId == bookId)
                                    .Select(c => c.Score)
                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == bookId).Count() * 100)) / 100
            }).SingleOrDefaultAsync(ct);

        return new GetBookResponse
        {
            Res = resut
        };
    }

    public async Task<GetBookCommentsResponse> GetBookComments(int bookId, CancellationToken ct)
    {
        var result = await _appDbContext.Comments
                        .Where(x => x.BookId == bookId)
                        .Select(x => new CommentDTO
                        {
                            Author = _appDbContext.Users.Where(u => u.Id == x.UserId).Select(u => u.Name).Single(),
                            Created = x.Created,
                            Id = x.Id,
                            Score = x.Score,
                            Text = x.Text,
                            Title = x.Title
                        }).ToArrayAsync(ct);

        return new GetBookCommentsResponse
        {
            Res = result
        };
    }

    public async Task<GetSearchedBookResponse> GetSearchedBook(string searchPatter, CancellationToken ct)
    {
        var result = await _appDbContext.Books
                        .Where(b => b.Title.Contains(searchPatter))
                        .Select(b => new BookDTO
                        {
                            AuthorId = b.AuthorId,
                            CountOfPages = b.CountOfPages,
                            CoverPath = b.CoverPath,
                            DateOfPublishing = b.DateOfPublishing,
                            DateOfWriting = b.DateOfWriting,
                            Description = b.Description,
                            Genre = b.Genre,
                            Name = b.Title,
                            Id = b.Id,
                            TotalScore = _appDbContext.Comments.Where(c => c.BookId == b.Id).Count() == 0 ? 0 :
                                    (Math.Truncate(_appDbContext.Comments
                                    .Where(c => c.BookId == b.Id)
                                    .Select(c => c.Score)
                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == b.Id).Count() * 100)) / 100
                        }).ToArrayAsync(ct);

        return new GetSearchedBookResponse
        {
            Res = result
        };
    }

    public async Task<bool> AddBookToFavorite(int bookId, HttpContext httpContext, CancellationToken ct)
    {
        if(_appDbContext.Books.Where(x => x.Id == bookId).Count() == 0)
        {
            return false;
        }

        var userId = await _appDbContext.Users
                        .Where(x => x.Email == httpContext.User
                        .FindFirstValue(ClaimTypes.Email))
                        .Select(x => x.Id)
                        .SingleOrDefaultAsync(ct);

        _appDbContext.UserBooks.Add(new Domain.Models.Entities.UserBooks
        {
            BookId = bookId,
            UserId = userId
        });

        await _appDbContext.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteBookFromFavorite(int bookId, HttpContext httpContext, CancellationToken ct)
    {
        if (_appDbContext.Books.Where(x => x.Id == bookId).Count() == 0)
        {
            return false;
        }

        var userId = await _appDbContext.Users
                        .Where(x => x.Email == httpContext.User
                        .FindFirstValue(ClaimTypes.Email))
                        .Select(x => x.Id)
                        .SingleOrDefaultAsync(ct);

        var book = await _appDbContext.UserBooks.Where(x => x.UserId == userId && x.BookId ==  bookId).SingleOrDefaultAsync(ct);

        if (book == null)
        {
            return false;
        }

        _appDbContext.UserBooks.Remove(book);

        await _appDbContext.SaveChangesAsync(ct);

        return true;
    }

    public async Task<GetAdminBooksResponse> GetAdminBooks(CancellationToken ct)
    {
        var result = await _appDbContext.Books
                        .Select(b => new ShortBookDTO
                        {
                            CoverPath = b.CoverPath,
                            DateOfPublishing = b.DateOfPublishing,
                            DateOfWriting = b.DateOfWriting,
                            Genre = b.Genre,
                            Id = b.Id,
                            Name = b.Title,
                            TotalScore = _appDbContext.Comments.Where(c => c.BookId == b.Id).Count() == 0 ? 0 :
                                    (Math.Truncate(_appDbContext.Comments
                                    .Where(c => c.BookId == b.Id)
                                    .Select(c => c.Score)
                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == b.Id).Count() * 100)) / 100
                        }).ToArrayAsync(ct);

        return new GetAdminBooksResponse
        {
            Res = result
        };
    }

    public async Task<int?> DeleteBook(int bookId, CancellationToken ct)
    {
        var book = await _appDbContext.Books.Where(x => x.Id == bookId).SingleOrDefaultAsync(ct);

        if (book == null)
        {
            return null;
        }

        var entity = _appDbContext.Books.Remove(book);

        await _appDbContext.SaveChangesAsync(ct);

        return entity.Entity.Id;
    }

    public async Task<PostBookResponse?> CreateBook(PostBookRequest req, CancellationToken ct)
    {
        if (req == null)
        {
            return null;
        }

        var book = _mapper.Map<Book>(req);

        var entity = _appDbContext.Books.Add(book);

        await _appDbContext.SaveChangesAsync(ct);

        var res = _mapper.Map<PostBookResponse>(entity.Entity);

        return res;
    }

    public async Task<PutBookResponse?> UpdateBook(int bookId, PutBookRequest req, CancellationToken ct)
    {
        var book = await _appDbContext.Books.Where(x => x.Id == bookId).SingleOrDefaultAsync(ct);

        if (book == null)
        {
            return null;
        }

        var result = _mapper.Map(req, book);

        _appDbContext.Books.Update(result);

        await _appDbContext.SaveChangesAsync(ct);

        var res = _mapper.Map<PutBookResponse>(result);

        return res;
    }
}
