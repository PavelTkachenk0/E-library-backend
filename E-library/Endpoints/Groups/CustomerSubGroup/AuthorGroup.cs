using FastEndpoints;

namespace E_library.Endpoints.Groups.CustomerSubGroup;

public class AuthorGroup : SubGroup<CustomerGroup>
{
    public AuthorGroup()
    {
        Configure("authors", ep =>
        {

        });
    }
}
