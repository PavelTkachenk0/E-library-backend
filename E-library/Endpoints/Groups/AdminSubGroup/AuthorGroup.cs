using FastEndpoints;

namespace E_library.Endpoints.Groups.AdminSubGroup;

public class AuthorGroup : SubGroup<AdminGroup>
{
    public AuthorGroup()
    {
        Configure("authors", ep =>
        {

        });
    }
}
