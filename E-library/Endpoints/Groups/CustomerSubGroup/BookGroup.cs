using FastEndpoints;

namespace E_library.Endpoints.Groups.CustomerSubGroup;

public class BookGroup : SubGroup<CustomerGroup>
{
    public BookGroup()
    {
        Configure("books", ep =>
        {

        });
    }
}
