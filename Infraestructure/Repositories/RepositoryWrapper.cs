using Domain.Interfaces;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private RepositoryContext _repoContext;

    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
        _repoContext = repositoryContext;
    }

    private IRecipeRepository _recipe;
    public IRecipeRepository Recipe
    {
        get { return _recipe = _recipe == null ? new RecipeRepository(_repoContext) : _recipe; }
    }
        

    public void Save()
    {
        _repoContext.SaveChanges();
    }

    public void SaveDetached()
    {
        _repoContext.SaveChanges();

        var changeEntries = _repoContext.ChangeTracker.Entries().ToList();
        foreach (var entry in changeEntries)
            entry.State = EntityState.Detached;
    }

    public void DetachAll()
    {
        foreach (var entry in _repoContext.ChangeTracker.Entries())
        {
            entry.State = EntityState.Detached;
        }
    }
        
    public void Dispose()
    {
        _repoContext.Dispose();
    }
}
