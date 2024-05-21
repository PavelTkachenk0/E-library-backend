using E_library.DAL;
using E_library.Domain.Constants;
using E_library.Mapping;
using E_library.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("Elibrary"));
});

builder.Services.AddControllers();

builder.Services.AddFastEndpoints(o =>
{
    o.SourceGeneratorDiscoveredTypes.AddRange(E.library.DiscoveredTypes.All);
});

builder.Services.SwaggerDocument(swagger =>
{
    swagger.EnableJWTBearerAuth = false;

});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy(PolicyNames.HasCustomerRole, p =>
    {
        p.RequireAssertion(c => c.User.IsInRole(Roles.Customer));
    });

    auth.AddPolicy(PolicyNames.HasAdminRole, p =>
    {
        p.RequireAssertion(c => c.User.IsInRole(Roles.Admin));
    });

    auth.AddPolicy(PolicyNames.HasCustomerAndAdminRole, p =>
    {
        p.RequireAssertion(c => c.User.IsInRole(Roles.Customer) || c.User.IsInRole(Roles.Admin));
    });
});

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<AuthorService>();

builder.Services.AddScoped<CommentService>();  

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(endpoints =>
{
    endpoints.Endpoints.RoutePrefix = "api";
    endpoints.Errors.StatusCode = 418;
}).UseSwaggerGen();

app.MapDefaultControllerRoute();

app.Run();