using AutoMapper;

using BookVoyage.Application.Authors;
using BookVoyage.Application.Books;
using BookVoyage.Application.Categories;
using BookVoyage.Application.Orders.Commands;
using BookVoyage.Application.Orders.Queries;
using BookVoyage.Application.ShoppingCarts.Queries;
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
        CreateMap<Author, AuthorEditDto>();
        CreateMap<AuthorEditDto, Author>();
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Category
                , opt => opt.MapFrom(src => new CategoryDto
            {
                Id = src.Category.Id,
                Name = src.Category.Name
            }))            
            .ForMember(dest => dest.Author
                , opt => opt.MapFrom(src =>  new AuthorEditDto
            {
                Id = src.Author.Id,
                FullName = src.Author.FullName,
                Publisher = src.Author.Publisher
            }));
        CreateMap<BookDto, Book>();
        CreateMap<BookUpsertDto, Book>();
        CreateMap<Book, BookUpsertDto>();
        CreateMap<ShoppingCart, ShoppingCartResponseDto>();
        CreateMap<ShoppingCartResponseDto, ShoppingCart>();
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString())); 
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => Enum.Parse(typeof(OrderStatus), src.OrderStatus))); 
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookOrderedItem.BookId))
            .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.BookOrderedItem.BookName))
            .ForMember(dest => dest.BookImageUrl, opt => opt.MapFrom(src => src.BookOrderedItem.ImageUrl));
        CreateMap<OrderItemDto, OrderItem>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.BookOrderedItem, opt => opt.MapFrom(src => new BookOrderedItem
            {
                BookId = src.BookId,
                BookName = src.BookName,
                ImageUrl = src.BookImageUrl
            }));
        CreateMap<OrderUpdatedDto, Order>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => Enum.Parse(typeof(OrderStatus), src.OrderStatus)));
        CreateMap<Order, OrderUpdatedDto>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));

    }
}