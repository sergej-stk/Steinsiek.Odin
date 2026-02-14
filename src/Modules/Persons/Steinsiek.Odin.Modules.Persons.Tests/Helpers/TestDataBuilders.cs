namespace Steinsiek.Odin.Modules.Persons.Tests.Helpers;

/// <summary>
/// Provides factory methods for creating test data in Persons module tests.
/// </summary>
internal static class TestDataBuilders
{
    /// <summary>
    /// Well-known person identifier for test data.
    /// </summary>
    public static readonly Guid DefaultPersonId = Guid.Parse("22222222-0001-0001-0001-000000000001");

    /// <summary>
    /// Creates a default person entity with sub-entities.
    /// </summary>
    public static Person CreateDefaultPerson()
    {
        var personId = DefaultPersonId;
        return new Person
        {
            Id = personId,
            FirstName = "Max",
            LastName = "Mustermann",
            Title = "Dr.",
            DateOfBirth = new DateTime(1990, 5, 15, 0, 0, 0, DateTimeKind.Utc),
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            EmailAddresses =
            [
                new PersonEmailAddress { Id = Guid.NewGuid(), PersonId = personId, Email = "max@example.com", IsPrimary = true }
            ],
            PhoneNumbers =
            [
                new PersonPhoneNumber { Id = Guid.NewGuid(), PersonId = personId, Number = "+49 123 456789", IsPrimary = true }
            ],
            Addresses =
            [
                new PersonAddress
                {
                    Id = Guid.NewGuid(), PersonId = personId, Street = "Main St 1", City = "Berlin",
                    PostalCode = "10115", CountryId = Guid.NewGuid(), IsPrimary = true
                }
            ],
            BankAccounts = [],
            SocialMediaLinks = [],
            Languages = []
        };
    }

    /// <summary>
    /// Creates a minimal person entity without sub-entities.
    /// </summary>
    public static Person CreateMinimalPerson()
    {
        return new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc),
            EmailAddresses = [],
            PhoneNumbers = [],
            Addresses = [],
            BankAccounts = [],
            SocialMediaLinks = [],
            Languages = []
        };
    }

    /// <summary>
    /// Creates a default create person request.
    /// </summary>
    public static CreatePersonRequest CreateDefaultCreateRequest()
    {
        return new CreatePersonRequest
        {
            FirstName = "New",
            LastName = "Person",
            Title = "Prof.",
            Notes = "Test notes"
        };
    }

    /// <summary>
    /// Creates a default update person request.
    /// </summary>
    public static UpdatePersonRequest CreateDefaultUpdateRequest()
    {
        return new UpdatePersonRequest
        {
            FirstName = "Updated",
            LastName = "Person",
            Title = "Dr.",
            Notes = "Updated notes"
        };
    }

    /// <summary>
    /// Creates a default paged query.
    /// </summary>
    public static PagedQuery CreateDefaultPagedQuery()
    {
        return new PagedQuery
        {
            Page = 1,
            PageSize = 25
        };
    }

    /// <summary>
    /// Creates a default person filter query.
    /// </summary>
    public static PersonFilterQuery CreateDefaultFilterQuery()
    {
        return new PersonFilterQuery();
    }
}
