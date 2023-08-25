using AutoMapper;
using FluentValidation;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Authors.Commands;

/// <summary>
/// A command and its handler to create an author
/// </summary>
public record CreateAuthorCommand : IRequest<ApiResult<Unit>>
{
    public AuthorDto AuthorDto { get; set; }
    
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAuthorCommand> _validator;

    public CreateAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateAuthorCommand> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        // Map the AuthorDto to an Author entity
        var authorEntity = _mapper.Map<Author>(request.AuthorDto);
        authorEntity.Books = null;
        // Add the entity to context
        _dbContext.Authors.Add(authorEntity);
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Author");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}