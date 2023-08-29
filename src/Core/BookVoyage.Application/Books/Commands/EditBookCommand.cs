using AutoMapper;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Utility.Constants;


namespace BookVoyage.Application.Books.Commands;

public record EditBookCommand: IRequest<ApiResult<Unit>>
{
    public BookUpsertDto BookEditDto { get; set; }
}

public class EditBookCommandHandler : IRequestHandler<EditBookCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<BookUpsertDto> _validator;
    private readonly IBlobService _blobService;

    public EditBookCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IValidator<BookUpsertDto> validator, IBlobService blobService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
        _blobService = blobService;
    }

    public async Task<ApiResult<Unit>> Handle(EditBookCommand request, CancellationToken cancellationToken)
    {
        // Validate form data
        await _validator.ValidateAndThrowAsync(request.BookEditDto, cancellationToken);
        // Get the update book from database
        var bookFromDb = await _dbContext.Books
                .Include(b => b.Author)
                .SingleOrDefaultAsync(b => b.Id == request.BookEditDto.Id, cancellationToken: cancellationToken);

        if (bookFromDb == null)
        {
            return ApiResult<Unit>.Failure("Book is not found");
        }

        // Check if the category is updated and update it if there is a change
        if (bookFromDb.CategoryId != request.BookEditDto.CategoryId)
        {
           var category = await _dbContext.Categories.FindAsync(request.BookEditDto.CategoryId);
            if (category == null)
            {
                return ApiResult<Unit>.Failure("Category is not found");
            }
            bookFromDb.Category = category;
        }

        // Check if authors have changed and update accordingly
        var currentAuthorId = bookFromDb.Author.Id;
        if (currentAuthorId != request.BookEditDto.AuthorId)
        {
            var author = await _dbContext.Authors.FindAsync(request.BookEditDto.AuthorId);
            bookFromDb.Author = author;
        }
        
        // Check if the image was changed 
        if (request.BookEditDto.File != null && request.BookEditDto.File.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.BookEditDto.File.FileName)}";
            bookFromDb.ImageUrl = await _blobService.UploadBlob(fileName, SD.SdStorageContainer,
                request.BookEditDto.File);
        }

        // Update the book
        _mapper.Map(request.BookEditDto, bookFromDb);
        
        // Save changes to the database
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        if (!result)
        {
            return ApiResult<Unit>.Failure("Failed to update the book");
        }
        
        return ApiResult<Unit>.Success(Unit.Value);
        
    }
}