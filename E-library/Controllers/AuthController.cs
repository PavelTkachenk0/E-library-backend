using E_library.Requests;
using E_library.Responses;
using E_library.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_library.Controllers;

public class AuthController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model, CancellationToken ct)
    {
        var result = await _authService.Login(model, ct);

        if (result == null)
        {
            return Unauthorized();
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result.ClaimsIdentity));

        return Ok(result.Resp);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/auth/register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model, CancellationToken ct)
    {
        var result = await _authService.Register(model, ct);

        if (result == null)
        {
            return Unauthorized();
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result.ClaimsIdentity));

        return Ok(result.Resp);
    }

    [HttpPost]
    [Route("api/auth/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}
