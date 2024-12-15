using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;

namespace Infraestructure.Repositories;

public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
{
    public RecipeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}