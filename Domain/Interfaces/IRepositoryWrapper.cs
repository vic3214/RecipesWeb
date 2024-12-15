
namespace Domain.Interfaces;

public interface IRepositoryWrapper : IDisposable
{
        
    IRecipeRepository Recipe { get; }
        
    void Save();
    void DetachAll();
    void SaveDetached();
}
