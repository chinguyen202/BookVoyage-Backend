using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Books.Commands;

public record DeleteBookCommand: IRequest<ApiResult<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteBookCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.FindAsync(request.Id);
        if (book == null) return null;
        _dbContext.Remove(book);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to delete the book");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}