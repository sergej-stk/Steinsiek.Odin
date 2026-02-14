namespace Steinsiek.Odin.Modules.Companies.Repositories;

/// <summary>
/// Repository interface for company entities with search support.
/// </summary>
public interface ICompanyRepository : IRepository<Company>
{
    /// <summary>
    /// Searches companies by name.
    /// </summary>
    /// <param name="searchTerm">The search term to match against company names.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of companies matching the search term.</returns>
    Task<IEnumerable<Company>> Search(string searchTerm, CancellationToken cancellationToken);
}
