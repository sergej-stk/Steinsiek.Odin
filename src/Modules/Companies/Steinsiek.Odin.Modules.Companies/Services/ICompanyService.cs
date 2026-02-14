namespace Steinsiek.Odin.Modules.Companies.Services;

/// <summary>
/// Service interface for company business operations.
/// </summary>
public interface ICompanyService
{
    /// <summary>
    /// Retrieves all companies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all companies.</returns>
    Task<ListResult<CompanyDto>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a company by its unique identifier with full details.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The company details if found; otherwise, null.</returns>
    Task<CompanyDetailDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new company.
    /// </summary>
    /// <param name="request">The company creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created company.</returns>
    Task<CompanyDto> Create(CreateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="request">The company update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated company if found; otherwise, null.</returns>
    Task<CompanyDto?> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a company by its unique identifier.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches companies by name.
    /// </summary>
    /// <param name="searchTerm">The search term to match against company names.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing matching companies.</returns>
    Task<ListResult<CompanyDto>> Search(string searchTerm, CancellationToken cancellationToken);
}
