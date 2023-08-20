using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Books;
using BookVoyage.Application.Categories;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Common.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorEditDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<AuthorEditDto, Author>();
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Category
                , opt => opt.MapFrom(src => new CategoryDto
            {
                Id = src.Category.Id,
                Name = src.Category.Name
            }))            
            .ForMember(dest => dest.Authors
                , opt => opt.MapFrom(src => src.Authors.Select(author => new AuthorEditDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Publisher = author.Publisher
            }).ToList()));
        CreateMap<BookDto, Book>();
        CreateMap<BookUpsertDto, Book>();
        CreateMap<Book, BookUpsertDto>();
    }
}