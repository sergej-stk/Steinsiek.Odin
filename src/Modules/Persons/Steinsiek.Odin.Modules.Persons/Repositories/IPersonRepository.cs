namespace Steinsiek.Odin.Modules.Persons.Repositories;

/// <summary>
/// Repository interface for person entities with search, paging, and filtering capability.
/// </summary>
public interface IPersonRepository : IRepository<Person>
{
    /// <summary>
    /// Searches persons by first name or last name matching the given search term.
    /// </summary>
    /// <param name="searchTerm">The search term to match against first and last names.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of persons matching the search criteria.</returns>
    Task<IEnumerable<Person>> Search(string searchTerm, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paged, filtered, and sorted collection of persons.
    /// </summary>
    /// <param name="query">The pagination and sorting parameters.</param>
    /// <param name="filter">The filter criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple of matching persons and the total count before pagination.</returns>
    Task<(IEnumerable<Person> Items, int TotalCount)> GetPaged(PagedQuery query, PersonFilterQuery filter, CancellationToken cancellationToken);
}
