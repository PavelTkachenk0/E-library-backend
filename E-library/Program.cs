using E_library.DAL;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("Elibrary"));
});

builder.Services.AddFastEndpoints(o =>
{
    o.SourceGeneratorDiscoveredTypes.AddRange(E.library.DiscoveredTypes.All);
});

builder.Services.SwaggerDocument(swagger =>
{
    swagger.EnableJWTBearerAuth = false;

});

//builder.Services.AddAuthorization(auth =>
//{
//    auth.FallbackPolicy = auth.DefaultPolicy;
//});

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseFastEndpoints(endpoints =>
{
    endpoints.Endpoints.RoutePrefix = "api";
    endpoints.Errors.StatusCode = 418;
}).UseSwaggerGen();

app.Run();