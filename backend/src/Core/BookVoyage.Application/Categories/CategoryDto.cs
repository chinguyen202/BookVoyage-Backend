namespace BookVoyage.Application.Categories;

public class CategoryDto
{
    public string Name { get; set; }
}

public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}