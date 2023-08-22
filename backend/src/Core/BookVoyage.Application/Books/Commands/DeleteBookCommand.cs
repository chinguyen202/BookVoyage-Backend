using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Persistence.Data;
using BookVoyage.Utility.Constants;
using MediatR;

namespace BookVoyage.Application.Books.Commands;

public record DeleteBookCommand: IRequest<ApiResult<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IBlobService _blobService;

    public DeleteBookCommandHandler(ApplicationDbContext dbContext, IBlobService blobService)
    {
        _dbContext = dbContext;
        _blobService = blobService;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var bookFromDb = await _dbContext.Books.FindAsync(request.Id);
        if (bookFromDb == null)
        {
            return ApiResult<Unit>.Failure("Can not find book.");
        }
        _dbContext.Remove(bookFromDb);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to delete the book");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}