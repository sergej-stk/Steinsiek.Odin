namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Client interface for company API operations.
/// </summary>
public interface ICompanyApiClient
{
    /// <summary>
    /// Retrieves all companies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list result containing companies, or <c>null</c> if the request failed.</returns>
    Task<ListResult<CompanyDto>?> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a company by its unique identifier including locations and person associations.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The company details, or <c>null</c> if not found.</returns>
    Task<CompanyDetailDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches companies by the specified search term.
    /// </summary>
    /// <param name="searchTerm">The search term to match against company names.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list result containing matching companies, or <c>null</c> if the request failed.</returns>
    Task<ListResult<CompanyDto>?> Search(string searchTerm, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new company.
    /// </summary>
    /// <param name="request">The company creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created company, or <c>null</c> if creation failed.</returns>
    Task<CompanyDto?> Create(CreateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="request">The company update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated company, or <c>null</c> if the update failed.</returns>
    Task<CompanyDto?> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a company by its unique identifier.
    /// </summary>
    /// <param name="id">The company identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if the company was deleted; otherwise, <c>false</c>.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}
