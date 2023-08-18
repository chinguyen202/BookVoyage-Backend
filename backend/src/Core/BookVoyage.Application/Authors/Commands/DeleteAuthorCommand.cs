using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Authors.Commands;

public record DeleteAuthorCommand: IRequest<ApiResult<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteAuthorCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FindAsync(request.Id);
        if (author == null) return null;
        _dbContext.Remove(author);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to delete the author");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}