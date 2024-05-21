using FastEndpoints;

namespace E_library.Endpoints.Groups.AdminSubGroup;

public class BookGroup : SubGroup<AdminGroup>
{
    public BookGroup()
    {
        Configure("books", ep =>
        {

        });
    }
}
