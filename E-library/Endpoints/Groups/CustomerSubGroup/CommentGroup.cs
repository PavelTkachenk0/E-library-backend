using FastEndpoints;

namespace E_library.Endpoints.Groups.CustomerSubGroup;

public class CommentGroup : SubGroup<CustomerGroup>
{
    public CommentGroup()
    {
        Configure("comments", ep =>
        {

        });
    }
}
