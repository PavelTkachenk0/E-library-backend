using E_library.Domain.Constants;
using FastEndpoints;

namespace E_library.Endpoints;

public class HelloWorldEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("");
        Policies(PolicyNames.HasCustomerAndAdminRole);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendStringAsync("Hello World!", cancellation: ct);
    }
}
