using AutoMapper;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record EditCategoryCommand: IRequest
{
    public Category Category { get; set; }
}

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public EditCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Category.Id);
        _mapper.Map(request.Category, category);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}