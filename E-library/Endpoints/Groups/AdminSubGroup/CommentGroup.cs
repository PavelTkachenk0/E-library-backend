using FastEndpoints;

namespace E_library.Endpoints.Groups.AdminSubGroup;

public class CommentGroup : SubGroup<AdminGroup>
{ 
    public CommentGroup()
    {
        Configure("comments", ep =>
        {

        });
    }
}
