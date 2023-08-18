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
        CreateMap<Category, CategoryUpdateDto>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorEditDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<AuthorEditDto, Author>();
        CreateMap<Book, BookDto>();
        CreateMap<Book, BookEditDto>();
        CreateMap<BookEditDto, Book>();
        CreateMap<BookDto, Book>();
    }
}