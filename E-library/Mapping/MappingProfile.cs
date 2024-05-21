using AutoMapper;
using E_library.Domain.Models.DTOs;
using E_library.Domain.Models.Entities;
using E_library.Endpoints.Customer.Books;
using E_library.Extensions;
using E_library.Requests;
using E_library.Responses;

namespace E_library.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PutBookRequest, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
            
        CreateMap<PostBookRequest, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));

        CreateMap<Book, PutBookResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

        CreateMap<Book, PostBookResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

        CreateMap<PostAuthorRequest, Author>();

        CreateMap<Author, ShortAuthorDTO>();

        CreateMap<PutAuthorRequest, Author>();
    }
}
