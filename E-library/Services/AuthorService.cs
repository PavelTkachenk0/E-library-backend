using AutoMapper;
using E_library.DAL;
using E_library.Domain.Models.DTOs;
using E_library.Domain.Models.Entities;
using E_library.Requests;
using E_library.Responses;
using Microsoft.EntityFrameworkCore;

namespace E_library.Services;

public class AuthorService(AppDbContext appDbContext, IMapper mapper)
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GetAuthorResponse?> GetAuthor(int authorId, CancellationToken ct)
    {
        var result = await _appDbContext.Authors
                .Where(a => a.Id == authorId)
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Surname = a.Surname,
                    Genre = a.Genre,
                    PhotoPath = a.PhotoPath,
                    ShortBio = a.ShortBio,
                    Books = _appDbContext.Books
                                .Where(b => b.AuthorId == authorId)
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
                                }).ToArray()
                }).SingleOrDefaultAsync(ct);

        if (result == null)
        {
            return null;
        }

        return new GetAuthorResponse
        {
            Res = result
        };
    }

    public async Task<GetAuthorsResponse> GetAuthors(CancellationToken ct)
    {
        var result = await _appDbContext.Authors.Select(x => new ShortAuthorDTO
        {
            Genre = x.Genre,
            Name = x.Name,
            Surname = x.Surname,
            Id = x.Id,
            PhotoPath = x.PhotoPath,
            ShortBio = x.ShortBio
        }).ToArrayAsync(ct);

        return new GetAuthorsResponse
        {
            Res = result
        };
    }

    public async Task<GetAdminAuthorResponse?> GetAdminAuthor(int authorId, CancellationToken ct)
    {
        var result = await _appDbContext.Authors
                        .Where(a => a.Id == authorId)
                        .Select(a => new ShortAuthorDTOWithBook
                        {
                            Genre = a.Genre,
                            Id = a.Id,
                            Surname = a.Surname,
                            Name = a.Name,
                            PhotoPath = a.PhotoPath,
                            ShortBio = a.ShortBio,
                            Books = _appDbContext.Books
                                        .Where(b => b.AuthorId == authorId)
                                        .Select(b => new AuthorBookDTO
                                        {
                                            Id = b.Id,
                                            Name = b.Title,
                                            TotalScore = _appDbContext.Comments.Where(c => c.BookId == b.Id).Count() == 0 ? 0 :
                                                    (Math.Truncate(_appDbContext.Comments
                                                    .Where(c => c.BookId == b.Id)
                                                    .Select(c => c.Score)
                                                    .Sum() / (double)_appDbContext.Comments.Where(c => c.BookId == b.Id).Count() * 100)) / 100
                                        }).ToArray()
                        }).SingleOrDefaultAsync(ct);

        if (result == null)
        {
            return null;
        }

        return new GetAdminAuthorResponse
        {
            Res = result
        };
    }

    public async Task<int?> DeleteAuthor(int authorId, CancellationToken ct)
    {
        var author = await _appDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefaultAsync(ct);

        if (author == null)
        {
            return null;
        }

        _appDbContext.Authors.Remove(author);

        await _appDbContext.SaveChangesAsync(ct);

        return author.Id;
    } 

    public async Task<PostAuthorResponse?> CreateAuthor(PostAuthorRequest req, CancellationToken ct)
    {
        if (req == null)
        {
            return null;
        }

        var author = _mapper.Map<Author>(req);

        var entity = _appDbContext.Authors.Add(author);

        await _appDbContext.SaveChangesAsync(ct);

        var res = _mapper.Map<ShortAuthorDTO>(entity.Entity);

        return new PostAuthorResponse
        {
            Res = res
        };
    }

    public async Task<PutAuthorResponse?> UpdateAuthor(int authorId, PutAuthorRequest req, CancellationToken ct)
    {
        var author = await _appDbContext.Authors.Where(x => x.Id == authorId).SingleOrDefaultAsync(ct);

        if (author == null)
        {
            return null;
        }

        var result = _mapper.Map(req, author);

        _appDbContext.Authors.Update(result);

        await _appDbContext.SaveChangesAsync(ct);

        var res = _mapper.Map<ShortAuthorDTO>(result);

        return new PutAuthorResponse
        {
            Res = res
        };
    }
}
