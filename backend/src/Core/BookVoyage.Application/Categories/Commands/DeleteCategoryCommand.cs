using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record DeleteCategoryCommand: IRequest
{
    public Guid Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteCategoryCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id);
        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}