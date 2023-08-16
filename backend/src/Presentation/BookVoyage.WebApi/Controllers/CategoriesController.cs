using BookVoyage.Application.Categories.Commands;
using Microsoft.AspNetCore.Mvc;

using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using BookVoyage.Application.Categories.Queries;

namespace BookVoyage.WebApi.Controllers;

// Controller for managing category-related API endpoints.
// {url}/api/categories
[ApiController]
public class CategoriesController: BaseApiController
{
    // Get all Categories
    [HttpGet(ApiEndpoints.Categories.GetAll)]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        return await Mediator.Send(new GetAllCategoriesQuery());
    }
    // Get a specific category
    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        return await Mediator.Send(new GetCategoryQuery { Id = id });
    }
    // Create a category
    [HttpPost(ApiEndpoints.Categories.Create)]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        return Ok(await Mediator.Send(new CreateCategoryCommand { Category = category }));
    }
    
    // Update a category
    [HttpPut(ApiEndpoints.Categories.Update)]
    public async Task<IActionResult> EditCategory(Guid id, Category category)
    {
        category.Id = id;
        return Ok(await Mediator.Send(new EditCategoryCommand { Category = category }));
    }
    
    // Delete a category
    [HttpDelete(ApiEndpoints.Categories.Delete)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteCategoryCommand{Id = id}));
    }
}