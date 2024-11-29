using BlogAspNet.Data;
using BlogAspNet.Extensions;
using BlogAspNet.Models;
using BlogAspNet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAspNet.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    /*
                *METODO GET
     */
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("05x8 - Falha interna no servidor"));
        }
    }
    
    /*
                *METODO GET ( APENAS 1 CATEGORIA )
     */
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        
            if (category == null)
                return NotFound(new ResultViewModel<Category>("Conteudo nao encontrado"));
        
            return Ok(new ResultViewModel<Category>(category));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05x9 - Falha interna no servidor"));
        }
    }
    
    /*
                *METODO POST
     */
    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] CreateCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };    
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE7 - Nao foi possivel incluir a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE7 - Falha interna no servidor"));
        }
    }
    
    /*
                *METODO PUT
     */
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] UpdateCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        
            if(category == null)
                return NotFound(new ResultViewModel<Category>("Conteudo nao encontrado"));

            category.Name = model.Name;
            category.Slug = model.Slug;
        
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE8 - Nao foi possivel alterar a categoria"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE8 - Falha interna no servidor"));
        }
    }

    /*
                *METODO DELETE
     */
    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Conteudo nao encontrado"));

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Nao foi possivel remover a categoria"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Falha interna no servidor"));
        }
    }
}