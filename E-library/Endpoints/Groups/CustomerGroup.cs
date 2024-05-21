using E_library.Domain.Constants;
using FastEndpoints;

namespace E_library.Endpoints.Groups;

public class CustomerGroup : Group
{
    public CustomerGroup()
    {
        Configure("customer", ep =>
        {
            ep.Policies(PolicyNames.HasCustomerRole);
        });
    }
}
