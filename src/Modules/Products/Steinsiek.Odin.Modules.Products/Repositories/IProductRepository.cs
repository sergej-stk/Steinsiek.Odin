namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Repository interface for product entities with category filtering and search support.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Retrieves all products belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of products in the specified category.</returns>
    Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for products by name or description.
    /// </summary>
    /// <param name="searchTerm">The search term to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of matching products.</returns>
    Task<IEnumerable<Product>> Search(string searchTerm, CancellationToken cancellationToken);
}
