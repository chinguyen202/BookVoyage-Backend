using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Authors.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetAuthorQuery : IRequest<ApiResult<AuthorDto>>
{
    public Guid Id { get; set; }
}

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, ApiResult<AuthorDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<AuthorDto>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
       var author = await _dbContext.Authors
           .FindAsync(request.Id);
       if (author == null)
       {
           return ApiResult<AuthorDto>.Failure("Can not find the author.");
       }
       var authorDto = _mapper.Map<AuthorDto>(author);
       return ApiResult<AuthorDto>.Success(authorDto);
    }
}