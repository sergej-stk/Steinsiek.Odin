namespace Steinsiek.Odin.Modules.Persons.Tests.Helpers;

/// <summary>
/// Extension methods for fluent mock setup in Persons module tests.
/// </summary>
internal static class MockSetupExtensions
{
    /// <summary>
    /// Sets up GetAll to return the given persons.
    /// </summary>
    public static Mock<IPersonRepository> SetupGetAll_Returns(this Mock<IPersonRepository> mock, IEnumerable<Person> persons)
    {
        mock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(persons);
        return mock;
    }

    /// <summary>
    /// Sets up GetById to return the given person.
    /// </summary>
    public static Mock<IPersonRepository> SetupGetById_Returns(this Mock<IPersonRepository> mock, Person? person)
    {
        mock.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);
        return mock;
    }

    /// <summary>
    /// Sets up Add to return the entity that was passed in.
    /// </summary>
    public static Mock<IPersonRepository> SetupAdd_ReturnsInput(this Mock<IPersonRepository> mock)
    {
        mock.Setup(r => r.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person p, CancellationToken _) => p);
        return mock;
    }

    /// <summary>
    /// Sets up Update to return the entity that was passed in.
    /// </summary>
    public static Mock<IPersonRepository> SetupUpdate_ReturnsInput(this Mock<IPersonRepository> mock)
    {
        mock.Setup(r => r.Update(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person p, CancellationToken _) => p);
        return mock;
    }

    /// <summary>
    /// Sets up Delete to return the given result.
    /// </summary>
    public static Mock<IPersonRepository> SetupDelete_Returns(this Mock<IPersonRepository> mock, bool result)
    {
        mock.Setup(r => r.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IPersonRepository.Search"/> to return the given persons.
    /// </summary>
    public static Mock<IPersonRepository> SetupSearch_Returns(this Mock<IPersonRepository> mock, IEnumerable<Person> persons)
    {
        mock.Setup(r => r.Search(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(persons);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IPersonRepository.GetPaged"/> to return the given result tuple.
    /// </summary>
    public static Mock<IPersonRepository> SetupGetPaged_Returns(
        this Mock<IPersonRepository> mock,
        IEnumerable<Person> items,
        int totalCount)
    {
        mock.Setup(r => r.GetPaged(It.IsAny<PagedQuery>(), It.IsAny<PersonFilterQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((items, totalCount));
        return mock;
    }
}
