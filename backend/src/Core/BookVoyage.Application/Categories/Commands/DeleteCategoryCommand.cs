using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record DeleteCategoryCommand: IRequest<ApiResult<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteCategoryCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id);
        if (category == null) return null;
        _dbContext.Remove(category);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to delete the category");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}