using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.WebApi.Controllers;

public class CategoriesController: BaseApiController
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // api/categories
    [HttpGet(ApiEndpoints.Categories.GetAll)]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }
    // api/categories/{:id}
    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }
}