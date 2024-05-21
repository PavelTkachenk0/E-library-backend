using E_library.Domain.Constants;
using FastEndpoints;

namespace E_library.Endpoints.Groups;

public class AdminGroup : Group
{
    public AdminGroup()
    {
        Configure("admin", ep =>
        {
            ep.Policies(PolicyNames.HasAdminRole);
        });
    }
}
