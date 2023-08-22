using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Books;
using BookVoyage.Application.Categories;
using BookVoyage.Application.Orders;
using BookVoyage.Domain.Entities;
using BookVoyage.Domain.Entities.OrderAggegate;

namespace BookVoyage.Application.Common.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Category
                , opt => opt.MapFrom(src => new CategoryDto
            {
                Id = src.Category.Id,
                Name = src.Category.Name
            }))            
            .ForMember(dest => dest.Authors
                , opt => opt.MapFrom(src => src.Authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                
                FullName = author.FullName,
                Publisher = author.Publisher
            }).ToList()));
        CreateMap<BookDto, Book>();
        CreateMap<BookUpsertDto, Book>();
        CreateMap<Book, BookUpsertDto>();
    }
}