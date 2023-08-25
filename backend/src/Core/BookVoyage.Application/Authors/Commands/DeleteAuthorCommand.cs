using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Authors.Commands;

/// <summary>
/// Command and its handler to delete an author
/// </summary>
public record DeleteAuthorCommand: IRequest<ApiResult<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteAuthorCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FindAsync(request.Id);
        if (author == null) return null;
        _dbContext.Authors.Remove(author);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to delete the author");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}