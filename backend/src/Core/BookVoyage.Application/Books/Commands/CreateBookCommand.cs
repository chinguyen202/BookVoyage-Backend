using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Books.Commands;

public record CreateBookCommand : IRequest<ApiResult<Unit>>
{
    public BookDto BookDto { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        // Map the BookDto to the Book entity
        var newBook = _mapper.Map<Book>(request.BookDto);
        // Get the category and author from the database
        var category = await _dbContext.Categories.FindAsync(request.BookDto.CategoryId);
        var author = await _dbContext.Authors.FindAsync(request.BookDto.AuthorId);
        if (category == null || author == null)
        {
            return ApiResult<Unit>.Failure("Failed to create book");
        }
        newBook.Author = author;
        newBook.Category = category;
        _dbContext.Books.Add(newBook);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Book");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}