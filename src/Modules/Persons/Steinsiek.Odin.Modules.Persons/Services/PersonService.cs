namespace Steinsiek.Odin.Modules.Persons.Services;

/// <summary>
/// Implementation of the person service handling CRUD and search operations.
/// </summary>
public sealed class PersonService(IPersonRepository personRepository, ILogger<PersonService> logger) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly ILogger<PersonService> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all persons");

        var persons = await _personRepository.GetAll(cancellationToken);
        var dtos = persons.Select(MapToDto).ToList();

        return new ListResult<PersonDto>
        {
            TotalCount = dtos.Count,
            Data = dtos
        };
    }

    /// <inheritdoc />
    public async Task<PersonDetailDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving person with Id {PersonId}", id);

        var person = await _personRepository.GetById(id, cancellationToken);
        if (person is null)
        {
            _logger.LogWarning("Person with Id {PersonId} not found", id);
            return null;
        }

        return MapToDetailDto(person);
    }

    /// <inheritdoc />
    public async Task<PersonDto> Create(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating person {FirstName} {LastName}", request.FirstName, request.LastName);

        var person = new Person
        {
            SalutationId = request.SalutationId,
            Title = request.Title,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            GenderId = request.GenderId,
            NationalityId = request.NationalityId,
            MaritalStatusId = request.MaritalStatusId,
            Notes = request.Notes
        };

        var created = await _personRepository.Add(person, cancellationToken);
        _logger.LogInformation("Person created with Id {PersonId}", created.Id);

        return MapToDto(created);
    }

    /// <inheritdoc />
    public async Task<PersonDto?> Update(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating person with Id {PersonId}", id);

        var person = await _personRepository.GetById(id, cancellationToken);
        if (person is null)
        {
            _logger.LogWarning("Person with Id {PersonId} not found for update", id);
            return null;
        }

        person.SalutationId = request.SalutationId;
        person.Title = request.Title;
        person.FirstName = request.FirstName;
        person.LastName = request.LastName;
        person.DateOfBirth = request.DateOfBirth;
        person.GenderId = request.GenderId;
        person.NationalityId = request.NationalityId;
        person.MaritalStatusId = request.MaritalStatusId;
        person.Notes = request.Notes;

        var updated = await _personRepository.Update(person, cancellationToken);
        _logger.LogInformation("Person with Id {PersonId} updated", id);

        return MapToDto(updated);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting person with Id {PersonId}", id);

        var result = await _personRepository.Delete(id, cancellationToken);
        if (!result)
        {
            _logger.LogWarning("Person with Id {PersonId} not found for deletion", id);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>> Search(string searchTerm, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching persons with term {SearchTerm}", searchTerm);

        var persons = await _personRepository.Search(searchTerm, cancellationToken);
        var dtos = persons.Select(MapToDto).ToList();

        return new ListResult<PersonDto>
        {
            TotalCount = dtos.Count,
            Data = dtos
        };
    }

    /// <summary>
    /// Maps a <see cref="Person"/> entity to a <see cref="PersonDto"/>.
    /// </summary>
    /// <param name="person">The person entity to map.</param>
    /// <returns>The mapped person summary DTO.</returns>
    private static PersonDto MapToDto(Person person)
    {
        var primaryEmail = person.EmailAddresses.FirstOrDefault(e => e.IsPrimary)
                           ?? person.EmailAddresses.FirstOrDefault();
        var primaryPhone = person.PhoneNumbers.FirstOrDefault(p => p.IsPrimary)
                           ?? person.PhoneNumbers.FirstOrDefault();
        var primaryAddress = person.Addresses.FirstOrDefault(a => a.IsPrimary)
                             ?? person.Addresses.FirstOrDefault();

        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Title = person.Title,
            Email = primaryEmail?.Email,
            Phone = primaryPhone?.Number,
            City = primaryAddress?.City
        };
    }

    /// <summary>
    /// Maps a <see cref="Person"/> entity to a <see cref="PersonDetailDto"/>.
    /// </summary>
    /// <param name="person">The person entity to map.</param>
    /// <returns>The mapped person detail DTO.</returns>
    private static PersonDetailDto MapToDetailDto(Person person)
    {
        return new PersonDetailDto
        {
            Id = person.Id,
            SalutationId = person.SalutationId,
            Title = person.Title,
            FirstName = person.FirstName,
            LastName = person.LastName,
            FullName = person.FullName,
            DateOfBirth = person.DateOfBirth,
            GenderId = person.GenderId,
            NationalityId = person.NationalityId,
            MaritalStatusId = person.MaritalStatusId,
            Notes = person.Notes,
            CreatedAt = person.CreatedAt,
            UpdatedAt = person.UpdatedAt,
            Addresses = person.Addresses.Select(MapToAddressDto).ToList(),
            EmailAddresses = person.EmailAddresses.Select(MapToEmailDto).ToList(),
            PhoneNumbers = person.PhoneNumbers.Select(MapToPhoneDto).ToList(),
            BankAccounts = person.BankAccounts.Select(MapToBankAccountDto).ToList(),
            SocialMediaLinks = person.SocialMediaLinks.Select(MapToSocialMediaDto).ToList()
        };
    }

    /// <summary>
    /// Maps a <see cref="PersonAddress"/> entity to a <see cref="PersonAddressDto"/>.
    /// </summary>
    /// <param name="address">The address entity to map.</param>
    /// <returns>The mapped address DTO.</returns>
    private static PersonAddressDto MapToAddressDto(PersonAddress address)
    {
        return new PersonAddressDto
        {
            Id = address.Id,
            AddressTypeId = address.AddressTypeId,
            Street = address.Street,
            Street2 = address.Street2,
            City = address.City,
            PostalCode = address.PostalCode,
            State = address.State,
            CountryId = address.CountryId,
            IsPrimary = address.IsPrimary
        };
    }

    /// <summary>
    /// Maps a <see cref="PersonEmailAddress"/> entity to a <see cref="PersonEmailDto"/>.
    /// </summary>
    /// <param name="email">The email address entity to map.</param>
    /// <returns>The mapped email DTO.</returns>
    private static PersonEmailDto MapToEmailDto(PersonEmailAddress email)
    {
        return new PersonEmailDto
        {
            Id = email.Id,
            Email = email.Email,
            Label = email.Label,
            IsPrimary = email.IsPrimary
        };
    }

    /// <summary>
    /// Maps a <see cref="PersonPhoneNumber"/> entity to a <see cref="PersonPhoneDto"/>.
    /// </summary>
    /// <param name="phone">The phone number entity to map.</param>
    /// <returns>The mapped phone DTO.</returns>
    private static PersonPhoneDto MapToPhoneDto(PersonPhoneNumber phone)
    {
        return new PersonPhoneDto
        {
            Id = phone.Id,
            Number = phone.Number,
            ContactTypeId = phone.ContactTypeId,
            Label = phone.Label,
            IsPrimary = phone.IsPrimary
        };
    }

    /// <summary>
    /// Maps a <see cref="PersonBankAccount"/> entity to a <see cref="PersonBankAccountDto"/>.
    /// </summary>
    /// <param name="bankAccount">The bank account entity to map.</param>
    /// <returns>The mapped bank account DTO.</returns>
    private static PersonBankAccountDto MapToBankAccountDto(PersonBankAccount bankAccount)
    {
        return new PersonBankAccountDto
        {
            Id = bankAccount.Id,
            Iban = bankAccount.Iban,
            Bic = bankAccount.Bic,
            BankName = bankAccount.BankName,
            Label = bankAccount.Label
        };
    }

    /// <summary>
    /// Maps a <see cref="PersonSocialMediaLink"/> entity to a <see cref="PersonSocialMediaDto"/>.
    /// </summary>
    /// <param name="socialMedia">The social media link entity to map.</param>
    /// <returns>The mapped social media DTO.</returns>
    private static PersonSocialMediaDto MapToSocialMediaDto(PersonSocialMediaLink socialMedia)
    {
        return new PersonSocialMediaDto
        {
            Id = socialMedia.Id,
            Platform = socialMedia.Platform,
            Url = socialMedia.Url
        };
    }
}
