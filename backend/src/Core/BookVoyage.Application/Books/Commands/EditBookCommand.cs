using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Books.Commands;

public record EditBookCommand: IRequest<ApiResult<Unit>>
{
    public BookEditDto BookEditDto { get; set; }
}

public class EditBookCommandHandler : IRequestHandler<EditBookCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public EditBookCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(EditBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.FindAsync(request.BookEditDto.Id);
        if (book == null) return null;
        _mapper.Map(request.BookEditDto, book);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to update the book");
        return ApiResult<Unit>.Success(Unit.Value) ;
    }
}