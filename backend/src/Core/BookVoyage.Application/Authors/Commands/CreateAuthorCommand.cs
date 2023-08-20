using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using FluentValidation;
using MediatR;

namespace BookVoyage.Application.Authors.Commands;

public record CreateAuthorCommand : IRequest<ApiResult<Unit>>
{
    public AuthorDto AuthorDto { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<AuthorDto> _validator;

    public CreateAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<AuthorDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.AuthorDto, cancellationToken);
        // Map the AuthorDto to an Author entity
        var authorEntity = _mapper.Map<Author>(request.AuthorDto);
        authorEntity.Books = null;
        // Add the entity to context
        _dbContext.Authors.Add(authorEntity);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Author");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}