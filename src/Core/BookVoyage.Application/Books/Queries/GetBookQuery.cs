using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Books.Queries;

/// <summary>
/// Handles a query to fetch a book by ID from the database.
/// </summary>
public record GetBookQuery : IRequest<ApiResult<BookDto>>
{
    public Guid Id { get; set; }
}

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ApiResult<BookDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
       var book = await _dbContext.Books
           .Include(a => a.Category)
           .Include(a => a.Authors)
           .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken: cancellationToken);
       if (book == null)
       {
           return ApiResult<BookDto>.Failure("Book can not be found.");
       }
       var bookDto = _mapper.Map<BookDto>(book);
       return ApiResult<BookDto>.Success(bookDto);
    }
}