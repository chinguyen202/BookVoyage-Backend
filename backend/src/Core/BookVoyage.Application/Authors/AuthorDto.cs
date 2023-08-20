using BookVoyage.Application.Books;

namespace BookVoyage.Application.Authors;

public class AuthorEditDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Publisher { get; set; }
}
public class AuthorDto: AuthorEditDto
{
    public BookDto BookDto { get; set; }
}

