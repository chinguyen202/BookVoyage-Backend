using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Books.Commands;

public record EditBookCommand: IRequest<ApiResult<Unit>>
{
    public BookUpsertDto BookEditDto { get; set; }
}

public class EditBookCommandHandler : IRequestHandler<EditBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<BookUpsertDto> _validator;

    public EditBookCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<BookUpsertDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ApiResult<Unit>> Handle(EditBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.BookEditDto, cancellationToken);
        var book = await _dbContext.Books
                .Include(b => b.Authors)
                .SingleOrDefaultAsync(b => b.Id == request.BookEditDto.Id);

        if (book == null)
        {
            return ApiResult<Unit>.Failure("Book is not found");
        }

            // Check if the category is updated and update it if there is a change
            if (book.CategoryId != request.BookEditDto.CategoryId)
            {
                var category = await _dbContext.Categories.FindAsync(request.BookEditDto.CategoryId);
                if (category == null)
                {
                    return ApiResult<Unit>.Failure("Category is not found");
                }

                book.Category = category;
            }

            // Check if authors have changed and update accordingly
            var currentAuthorIds = book.Authors.Select(a => a.Id).ToList();
            if (!currentAuthorIds.SequenceEqual(request.BookEditDto.AuthorIds))
            {
                var authors = await _dbContext.Authors
                    .Where(a => request.BookEditDto.AuthorIds.Contains(a.Id))
                    .ToListAsync();
                book.Authors.Clear();
                book.Authors.AddRange(authors);
            }

            // Update the book
            _mapper.Map(request.BookEditDto, book);

            // Save changes to the database
            var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return ApiResult<Unit>.Failure("Failed to update the book");
            }

            return ApiResult<Unit>.Success(Unit.Value);
        
    }
}