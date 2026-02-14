namespace Steinsiek.Odin.Modules.Persons.Repositories;

/// <summary>
/// Repository interface for person entities with search capability.
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
}
