using Application.DTO;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RecipesAPI.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    // GET: api/recipes
    [HttpGet]
    public IActionResult GetAllRecipes(int page = 1, int pageSize = 10)
    {
        try
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { Message = "Page and pageSize must be greater than zero." });

            PaginatedResult<RecipeDTO> paginatedRecipes = _recipeService.GetAllRecipes(page, pageSize);
            return Ok(paginatedRecipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while fetching recipes.", Details = ex.Message });
        }
    }

    [HttpGet("search")]
    public IActionResult GetRecipesBySearch([FromQuery(Name = "searchText")] string searchText)
    {
        try
        {
            searchText = searchText.ToLower();
            List<RecipeDTO> recipes = _recipeService.GetRecipesBySearch(searchText);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while fetching recipes.", Details = ex.Message });
        }
    }


    // GET: api/recipes/{id}
    [HttpGet("{id}")]
    public IActionResult GetRecipeById(int id)
    {
        try
        {
            RecipeDTO recipe = _recipeService.GetRecipeById(id);
            if (recipe == null) return NotFound(new { Message = "Recipe not found" });
            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while fetching the recipe.", Details = ex.Message });
        }
    }

    // POST: api/recipes
    [HttpPost]
    public IActionResult CreateRecipe([FromBody] Recipe recipe)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int createdId = _recipeService.CreateRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = createdId }, recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while creating the recipe.", Details = ex.Message });
        }
    }

    // PUT: api/recipes/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateRecipe(int id, [FromBody] Recipe recipe)
    {
        try
        {
            if (id != recipe.Id) return BadRequest(new { Message = "ID mismatch" });

            RecipeDTO existingRecipe = _recipeService.GetRecipeById(id);
            if (existingRecipe == null) return NotFound(new { Message = "Recipe not found" });

            _recipeService.UpdateRecipe(recipe);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while updating the recipe.", Details = ex.Message });
        }
    }

    // DELETE: api/recipes/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteRecipe(int id)
    {
        try
        {
            RecipeDTO recipe = _recipeService.GetRecipeById(id);
            if (recipe == null) return NotFound(new { Message = "Recipe not found" });

            _recipeService.DeleteRecipeById(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while deleting the recipe.", Details = ex.Message });
        }
    }
}
