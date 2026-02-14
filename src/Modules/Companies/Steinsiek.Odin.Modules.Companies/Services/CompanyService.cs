namespace Steinsiek.Odin.Modules.Companies.Services;

/// <summary>
/// Implementation of the company service handling business logic for company operations.
/// </summary>
public sealed class CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger) : ICompanyService
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly ILogger<CompanyService> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<CompanyDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all companies");

        var companies = await _companyRepository.GetAll(cancellationToken);
        var companyList = companies.ToList();

        return new ListResult<CompanyDto>
        {
            TotalCount = companyList.Count,
            Data = companyList.Select(MapToDto).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<CompanyDetailDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving company {CompanyId}", id);

        var company = await _companyRepository.GetById(id, cancellationToken);
        if (company is null)
        {
            _logger.LogWarning("Company {CompanyId} not found", id);
            return null;
        }

        return MapToDetailDto(company);
    }

    /// <inheritdoc />
    public async Task<CompanyDto> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating company {CompanyName}", request.Name);

        var company = new Company
        {
            Name = request.Name,
            LegalFormId = request.LegalFormId,
            IndustryId = request.IndustryId,
            Website = request.Website,
            Email = request.Email,
            Phone = request.Phone,
            TaxNumber = request.TaxNumber,
            VatId = request.VatId,
            CommercialRegisterNumber = request.CommercialRegisterNumber,
            FoundingDate = request.FoundingDate,
            EmployeeCount = request.EmployeeCount,
            Revenue = request.Revenue,
            ParentCompanyId = request.ParentCompanyId,
            Notes = request.Notes
        };

        var created = await _companyRepository.Add(company, cancellationToken);
        _logger.LogInformation("Company {CompanyId} created successfully", created.Id);

        return MapToDto(created);
    }

    /// <inheritdoc />
    public async Task<CompanyDto?> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating company {CompanyId}", id);

        var company = await _companyRepository.GetById(id, cancellationToken);
        if (company is null)
        {
            _logger.LogWarning("Company {CompanyId} not found for update", id);
            return null;
        }

        company.Name = request.Name;
        company.LegalFormId = request.LegalFormId;
        company.IndustryId = request.IndustryId;
        company.Website = request.Website;
        company.Email = request.Email;
        company.Phone = request.Phone;
        company.TaxNumber = request.TaxNumber;
        company.VatId = request.VatId;
        company.CommercialRegisterNumber = request.CommercialRegisterNumber;
        company.FoundingDate = request.FoundingDate;
        company.EmployeeCount = request.EmployeeCount;
        company.Revenue = request.Revenue;
        company.ParentCompanyId = request.ParentCompanyId;
        company.Notes = request.Notes;

        var updated = await _companyRepository.Update(company, cancellationToken);
        _logger.LogInformation("Company {CompanyId} updated successfully", id);

        return MapToDto(updated);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting company {CompanyId}", id);

        var result = await _companyRepository.Delete(id, cancellationToken);
        if (!result)
        {
            _logger.LogWarning("Company {CompanyId} not found for deletion", id);
        }
        else
        {
            _logger.LogInformation("Company {CompanyId} deleted successfully", id);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ListResult<CompanyDto>> Search(string searchTerm, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching companies with term {SearchTerm}", searchTerm);

        var companies = await _companyRepository.Search(searchTerm, cancellationToken);
        var companyList = companies.ToList();

        return new ListResult<CompanyDto>
        {
            TotalCount = companyList.Count,
            Data = companyList.Select(MapToDto).ToList()
        };
    }

    /// <summary>
    /// Maps a company entity to a summary DTO.
    /// </summary>
    /// <param name="company">The company entity.</param>
    /// <returns>The company summary DTO.</returns>
    private static CompanyDto MapToDto(Company company)
    {
        var primaryLocation = company.Locations.FirstOrDefault(l => l.IsPrimary)
            ?? company.Locations.FirstOrDefault();

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Website = company.Website,
            City = primaryLocation?.City,
            EmployeeCount = company.EmployeeCount
        };
    }

    /// <summary>
    /// Maps a company entity to a detailed DTO including locations and person associations.
    /// </summary>
    /// <param name="company">The company entity.</param>
    /// <returns>The company detail DTO.</returns>
    private static CompanyDetailDto MapToDetailDto(Company company)
    {
        return new CompanyDetailDto
        {
            Id = company.Id,
            Name = company.Name,
            LegalFormId = company.LegalFormId,
            IndustryId = company.IndustryId,
            Website = company.Website,
            Email = company.Email,
            Phone = company.Phone,
            TaxNumber = company.TaxNumber,
            VatId = company.VatId,
            CommercialRegisterNumber = company.CommercialRegisterNumber,
            FoundingDate = company.FoundingDate,
            EmployeeCount = company.EmployeeCount,
            Revenue = company.Revenue,
            ParentCompanyId = company.ParentCompanyId,
            Notes = company.Notes,
            Locations = company.Locations.Select(l => new CompanyLocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Street = l.Street,
                Street2 = l.Street2,
                City = l.City,
                PostalCode = l.PostalCode,
                State = l.State,
                CountryId = l.CountryId,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Phone = l.Phone,
                Email = l.Email,
                IsPrimary = l.IsPrimary
            }).ToList(),
            PersonCompanies = company.PersonCompanies.Select(pc => new PersonCompanyDto
            {
                Id = pc.Id,
                PersonId = pc.PersonId,
                CompanyId = pc.CompanyId,
                PositionId = pc.PositionId,
                DepartmentId = pc.DepartmentId,
                StartDate = pc.StartDate,
                EndDate = pc.EndDate,
                IsActive = pc.IsActive
            }).ToList()
        };
    }
}
