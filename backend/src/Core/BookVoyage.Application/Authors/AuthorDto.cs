using BookVoyage.Application.Books;

namespace BookVoyage.Application.Authors;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Publisher { get; set; }
}


