using AutoMapper;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;

namespace BookVoyage.Application.Books.Commands;

public record CreateBookCommand : IRequest<ApiResult<Unit>>
{
    public BookUpsertDto BookUpsertDto { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<BookUpsertDto> _validator;
    private readonly IBlobService _blobService;

    public CreateBookCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IValidator<BookUpsertDto> validator, IBlobService blobService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
        _blobService = blobService;
    }
    public async Task<ApiResult<Unit>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.BookUpsertDto, cancellationToken);
        // Map the BookDto to the Book entity
        var newBook = _mapper.Map<Book>(request.BookUpsertDto);
        // Upload the image
        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.BookUpsertDto.File.Name)}";
        var imageUrl = await _blobService.UploadBlob(fileName,SD.SdStorageContainer,request.BookUpsertDto.File);
        newBook.ImageUrl = imageUrl;
        // Fetch the category from db if exists
        var category = await _dbContext.Categories.FindAsync(request.BookUpsertDto.CategoryId);
        newBook.Category = category;
        // Fetch the authors from db if exists
        var authors = await _dbContext.Authors
            .Where(author => request.BookUpsertDto.AuthorIds
            .Contains(author.Id))
            .ToListAsync(cancellationToken: cancellationToken);
        newBook.Authors.AddRange(authors);
        _dbContext.Books.Add(newBook);
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Book");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}

