using Application.DTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Mapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class RecipeService : IRecipeService
{
    private readonly IRepositoryWrapper _repoWrapper;

    public RecipeService(IRepositoryWrapper repoWrapper)
    {
        _repoWrapper = repoWrapper;
    }

    List<RecipeDTO> IRecipeService.GetRecipesBySearch(string searchText)
    {
        searchText = searchText.ToLower();

        // Utilizando el método FindByCondition para aprovechar la lógica común
        List<Recipe> recipes = _repoWrapper.Recipe.FindByCondition(r => r.Title.ToLower().Contains(searchText), null)
            .Take(20)
            .ToList();

        List<RecipeDTO> returnValue = recipes
            .Select(recipe => EntityToDTO.RecipeToDTO(recipe))
            .ToList();

        return returnValue;
    }


    RecipeDTO IRecipeService.GetRecipeById(int id)
    {
        Recipe recipe = _repoWrapper.Recipe.FindByCondition(recipe => recipe.Id == id, null).First();
        return EntityToDTO.RecipeToDTO(recipe);
    }

    int IRecipeService.CreateRecipe(Recipe recipe)
    {
        int companyDbId = _repoWrapper.Recipe.Create(recipe);

        _repoWrapper.Save();

        return companyDbId;
    }

    void IRecipeService.UpdateRecipe(Recipe recipe)
    {
        _repoWrapper.Recipe.Update(recipe);
        _repoWrapper.Save();
    }

    void IRecipeService.DeleteRecipe(Recipe recipe)
    {
        _repoWrapper.Recipe.Delete(recipe);
        _repoWrapper.Save();
    }

    void IRecipeService.DeleteRecipeById(int id)
    {
        _repoWrapper.Recipe.Delete(id);
        _repoWrapper.Save();
    }

    public PaginatedResult<RecipeDTO> GetAllRecipes(int page, int pageSize)
    {
        int totalItems = _repoWrapper.Recipe.FindAll(null).Count(); // Total de recetas en la base de datos.
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize); // Calcular el total de páginas.

        List<Recipe> recipes = _repoWrapper.Recipe.FindAll(null)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        List<RecipeDTO> recipeDTOs = recipes
            .Select(recipe => EntityToDTO.RecipeToDTO(recipe))
            .ToList();

        return new PaginatedResult<RecipeDTO>
        {
            Data = recipeDTOs,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize
        };
    }
}
