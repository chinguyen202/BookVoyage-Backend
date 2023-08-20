using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using FluentValidation;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record EditAuthorCommand: IRequest<ApiResult<Unit>>
{
    public AuthorDto AuthorEditDto { get; set; }
}

public class EditAuthorCommandHandler : IRequestHandler<EditAuthorCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<AuthorDto> _validator;

    public EditAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<AuthorDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.AuthorEditDto, cancellationToken);
        var author = await _dbContext.Authors.FindAsync(request.AuthorEditDto.Id);
        if (author == null) return ApiResult<Unit>.Failure("Author can't be found.");
        _mapper.Map(request.AuthorEditDto, author);
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to update the author");
        return ApiResult<Unit>.Success(Unit.Value) ;
    }
}