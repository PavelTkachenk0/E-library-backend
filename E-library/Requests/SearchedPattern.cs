using FastEndpoints;

namespace E_library.Requests;

public class Pattern
{
    [QueryParam]
    public string SearchedPattern { get; set; } = null!;
}
