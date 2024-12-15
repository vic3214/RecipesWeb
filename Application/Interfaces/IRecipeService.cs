using Application.DTO;
using Application.Helpers;
using Domain.Entities;

namespace Application.Interfaces;

public interface IRecipeService
{
    PaginatedResult<RecipeDTO> GetAllRecipes(int page, int pageSize);

    List<RecipeDTO> GetRecipesBySearch(string searchText);

    RecipeDTO GetRecipeById(int id);

    int CreateRecipe(Recipe recipe);

    void UpdateRecipe(Recipe recipe);

    void DeleteRecipeById(int id);

    void DeleteRecipe(Recipe recipe);
}
