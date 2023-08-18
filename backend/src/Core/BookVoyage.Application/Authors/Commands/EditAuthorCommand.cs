using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record EditAuthorCommand: IRequest<ApiResult<Unit>>
{
    public AuthorEditDto AuthorEditDto { get; set; }
}

public class EditAuthorCommandHandler : IRequestHandler<EditAuthorCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public EditAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FindAsync(request.AuthorEditDto.Id);
        if (author == null) return null;
        _mapper.Map(request.AuthorEditDto, author);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to update the author");
        return ApiResult<Unit>.Success(Unit.Value) ;
    }
}