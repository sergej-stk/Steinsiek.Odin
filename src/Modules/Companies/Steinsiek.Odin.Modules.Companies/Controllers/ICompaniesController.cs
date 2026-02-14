namespace Steinsiek.Odin.Modules.Companies.Controllers;

/// <summary>
/// Defines the contract for company management operations.
/// </summary>
public interface ICompaniesController
{
    /// <summary>
    /// Retrieves all companies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all companies.</returns>
    Task<ActionResult<ListResult<CompanyDto>>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a company by its unique identifier.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The company details if found.</returns>
    Task<ActionResult<CompanyDetailDto>> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches companies by name.
    /// </summary>
    /// <param name="q">The search term.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing matching companies.</returns>
    Task<ActionResult<ListResult<CompanyDto>>> Search(string? q, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new company.
    /// </summary>
    /// <param name="request">The company creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created company.</returns>
    Task<ActionResult<CompanyDto>> Create(CreateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="request">The company update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated company if found.</returns>
    Task<ActionResult<CompanyDto>> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paged, filtered, and sorted collection of companies.
    /// </summary>
    /// <param name="query">The pagination and sorting parameters.</param>
    /// <param name="filter">The filter criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paged result of companies.</returns>
    Task<ActionResult<PagedResult<CompanyDto>>> GetPaged(PagedQuery query, CompanyFilterQuery filter, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a company by its unique identifier.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if deleted successfully.</returns>
    Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes multiple companies by their identifiers.
    /// </summary>
    /// <param name="ids">The collection of company identifiers to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of successfully deleted companies.</returns>
    Task<ActionResult<int>> DeleteMany(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
}
