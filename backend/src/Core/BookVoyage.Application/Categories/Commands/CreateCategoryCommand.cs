using AutoMapper;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record CreateCategoryCommand : IRequest
{
    public Category Category { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        _dbContext.Categories.Add(request.Category);
        Console.WriteLine(request.Category);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}