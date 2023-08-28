using AutoMapper;
using FluentValidation;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Authors.Commands;

public record EditAuthorCommand: IRequest<ApiResult<Unit>>
{
    public AuthorEditDto AuthorEditDto { get; set; }
}

public class EditAuthorCommandHandler : IRequestHandler<EditAuthorCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<EditAuthorCommand> _validator;

    public EditAuthorCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IValidator<EditAuthorCommand> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var author = await _dbContext.Authors.FindAsync(request.AuthorEditDto.Id);
        if (author == null) return ApiResult<Unit>.Failure("Author can't be found.");
        _mapper.Map(request.AuthorEditDto, author);
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to update the author");
        return ApiResult<Unit>.Success(Unit.Value) ;
    }
}