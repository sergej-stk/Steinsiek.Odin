namespace Steinsiek.Odin.Modules.Companies.Tests.Helpers;

/// <summary>
/// Extension methods for fluent mock setup in Companies module tests.
/// </summary>
internal static class MockSetupExtensions
{
    /// <summary>
    /// Sets up GetAll to return the given companies.
    /// </summary>
    public static Mock<ICompanyRepository> SetupGetAll_Returns(this Mock<ICompanyRepository> mock, IEnumerable<Company> companies)
    {
        mock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(companies);
        return mock;
    }

    /// <summary>
    /// Sets up GetById to return the given company.
    /// </summary>
    public static Mock<ICompanyRepository> SetupGetById_Returns(this Mock<ICompanyRepository> mock, Company? company)
    {
        mock.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(company);
        return mock;
    }

    /// <summary>
    /// Sets up Add to return the entity that was passed in.
    /// </summary>
    public static Mock<ICompanyRepository> SetupAdd_ReturnsInput(this Mock<ICompanyRepository> mock)
    {
        mock.Setup(r => r.Add(It.IsAny<Company>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company c, CancellationToken _) => c);
        return mock;
    }

    /// <summary>
    /// Sets up Update to return the entity that was passed in.
    /// </summary>
    public static Mock<ICompanyRepository> SetupUpdate_ReturnsInput(this Mock<ICompanyRepository> mock)
    {
        mock.Setup(r => r.Update(It.IsAny<Company>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company c, CancellationToken _) => c);
        return mock;
    }

    /// <summary>
    /// Sets up Delete to return the given result.
    /// </summary>
    public static Mock<ICompanyRepository> SetupDelete_Returns(this Mock<ICompanyRepository> mock, bool result)
    {
        mock.Setup(r => r.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="ICompanyRepository.Search"/> to return the given companies.
    /// </summary>
    public static Mock<ICompanyRepository> SetupSearch_Returns(this Mock<ICompanyRepository> mock, IEnumerable<Company> companies)
    {
        mock.Setup(r => r.Search(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(companies);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="ICompanyRepository.GetPaged"/> to return the given result tuple.
    /// </summary>
    public static Mock<ICompanyRepository> SetupGetPaged_Returns(
        this Mock<ICompanyRepository> mock,
        IEnumerable<Company> items,
        int totalCount)
    {
        mock.Setup(r => r.GetPaged(It.IsAny<PagedQuery>(), It.IsAny<CompanyFilterQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((items, totalCount));
        return mock;
    }
}
