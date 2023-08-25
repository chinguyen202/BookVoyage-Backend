using BookVoyage.Application.Books;

namespace BookVoyage.Application.Authors;


public class AuthorDto
{
    public string FullName { get; set; }
    public string Publisher { get; set; }
}

public class AuthorEditDto: AuthorDto
{
    public Guid Id { get; set; }
}