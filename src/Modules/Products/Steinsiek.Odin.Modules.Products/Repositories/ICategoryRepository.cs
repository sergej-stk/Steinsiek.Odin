namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Repository interface for category entities with name lookup support.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Retrieves a category by its name.
    /// </summary>
    /// <param name="name">The category name to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The category if found; otherwise, null.</returns>
    Task<Category?> GetByName(string name, CancellationToken cancellationToken);
}
