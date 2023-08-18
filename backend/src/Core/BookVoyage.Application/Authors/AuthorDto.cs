namespace BookVoyage.Application.Authors;


public class AuthorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class AuthorEditDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}