using AutoMapper;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Books.Commands;

public record CreateBookCommand : IRequest<ApiResult<Unit>>
{
    public BookUpsertDto BookUpsertDto { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<BookUpsertDto> _validator;

    public CreateAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<BookUpsertDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.BookUpsertDto, cancellationToken);
        // Map the BookDto to the Book entity
        var newBook = _mapper.Map<Book>(request.BookUpsertDto);
        // Fetch the category from db if exists
        var category = await _dbContext.Categories.FindAsync(request.BookUpsertDto.CategoryId);
        newBook.Category = category;
        // Fetch the authors from db if exists
        var authors = await _dbContext.Authors
            .Where(author => request.BookUpsertDto.AuthorIds
            .Contains(author.Id))
            .ToListAsync();
        newBook.Authors.AddRange(authors);
        _dbContext.Books.Add(newBook);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Book");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}

