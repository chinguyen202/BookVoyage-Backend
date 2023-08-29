using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookVoyage.Application.Categories;
using BookVoyage.Application.Categories.Commands;
using BookVoyage.Utility.Constants;
using BookVoyage.Application.Categories.Queries;

namespace BookVoyage.WebApi.Controllers;

// Controller for managing category-related API endpoints.
// {url}/api/categories
[ApiController]
public class CategoriesController: BaseApiController
{
    //  Get all Categories
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Categories.GetAll)]
    public async Task<IActionResult> GetCategories()
    {
        return HandleResult(await Mediator.Send(new GetAllCategoriesQuery())) ;
    }
    //  Get a specific category
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Categories.Get)]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetCategoryQuery { Id = id }));
    }
    //  Create a category 
    [Authorize(Roles = SD.Admin)]
    [HttpPost(ApiEndpoints.V1.Categories.Create)]
    public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
    {
        return HandleResult(await Mediator.Send(new CreateCategoryCommand { CategoryDto = categoryDto }));
    }
    
    // Update a category
    [Authorize(Roles = SD.Admin)]
    [HttpPut(ApiEndpoints.V1.Categories.Update)]
    public async Task<IActionResult> EditCategory(Guid id, CategoryDto categoryDto)
    {
        categoryDto.Id = id;
        return HandleResult(await Mediator.Send(new EditCategoryCommand { CategoryUpdate = categoryDto }));
    }
    
    // Delete a category
    [Authorize(Roles = SD.Admin)]
    [HttpDelete(ApiEndpoints.V1.Categories.Delete)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteCategoryCommand{Id = id}));
    }
}