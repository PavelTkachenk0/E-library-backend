using E_library.DAL;
using E_library.Domain.Models.Entities;
using E_library.Requests;
using E_library.Responses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_library.Services;

public class AuthService(AppDbContext appDbContext)
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<LoginResponse?> Login(LoginRequest req, CancellationToken ct)
    {
        var user = await _appDbContext.Users
            .Where(x => x.Email == req.Email && x.Password == req.Password)
            .Select(x => new
            {
                x.Email,
                x.Name,
                Roles = x.Roles
                    .Select(x => x.Name)
                    .ToArray()
            }).SingleOrDefaultAsync(ct);

        if (user == null)
        {
            return null!;
        }

        if (!user.Roles.Any())
        {
            return null!;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name)
        };

        var roleClaims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role));

        claims.AddRange(roleClaims);

        ClaimsIdentity claimsIdentity = new(claims, "Cookies");

        var authResponse = new AuthResponse
        {
            Email = req.Email,
            Password = req.Password,
            Name = user.Name,
            Roles = user.Roles.Select(role => new Claim(ClaimTypes.Role, role).Value).ToArray()
        };

        return new LoginResponse
        {
            Resp = authResponse,
            ClaimsIdentity = claimsIdentity
        };
    }

    public async Task<LoginResponse?> Register(RegisterRequest req, CancellationToken ct)
    {
        var userEntity = _appDbContext.Users.Add(new User
        {
            Email = req.Email,
            Password = req.Password,
            Name = req.Name,
        });

        await _appDbContext.SaveChangesAsync(ct);

        await _appDbContext.UserRoles
            .AddAsync(new UserRoles
            {
                UserId = userEntity.Entity.Id,
                RoleId = 2
            }, ct);

        await _appDbContext.SaveChangesAsync(ct);

        var loginReq = new LoginRequest
        {
            Email = req.Email,
            Password = req.Password,
        };

        var userLogin = await Login(loginReq, ct);

        return userLogin;
    }
}

public class LoginResponse
{
    public AuthResponse Resp { get; set; } = null!;
    public ClaimsIdentity ClaimsIdentity { get; set; } = null!;
}