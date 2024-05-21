using AutoMapper;

namespace E_library.Extensions;

public class NullIgnoreResolver<TSource, TDestination, TSourceMember> : IMemberValueResolver<TSource, TDestination, TSourceMember, TSourceMember>
{
    public TSourceMember Resolve(TSource source, TDestination destination, TSourceMember sourceMember, TSourceMember destMember, ResolutionContext context)
    {
        return sourceMember == null ? destMember : sourceMember;
    }
}
