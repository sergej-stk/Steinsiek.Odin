namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Translation"/> including all lookup translation seed data.
/// </summary>
public sealed class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.ToTable("Translations", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EntityId)
            .IsRequired();

        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.LanguageCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(e => new { e.EntityId, e.LanguageCode });
        builder.HasIndex(e => new { e.EntityType, e.LanguageCode });

        SeedSalutationTranslations(builder);
        SeedGenderTranslations(builder);
        SeedMaritalStatusTranslations(builder);
        SeedCountryTranslations(builder);
        SeedAddressTypeTranslations(builder);
        SeedContactTypeTranslations(builder);
        SeedIndustryTranslations(builder);
        SeedLegalFormTranslations(builder);
        SeedDepartmentTranslations(builder);
        SeedPositionTranslations(builder);
    }

    /// <summary>
    /// Seeds translation data for <see cref="Salutation"/> entities.
    /// </summary>
    private static void SeedSalutationTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000010-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000010-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000010-0001-0001-0001-000000000003");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Salutation", LanguageCode = "de", Value = "Herr" },
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Salutation", LanguageCode = "en", Value = "Mr" },
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Salutation", LanguageCode = "de", Value = "Frau" },
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Salutation", LanguageCode = "en", Value = "Mrs" },
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Salutation", LanguageCode = "de", Value = "Divers" },
            new Translation { Id = Guid.Parse("00000010-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Salutation", LanguageCode = "en", Value = "Mx" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="Gender"/> entities.
    /// </summary>
    private static void SeedGenderTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000020-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000020-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000020-0001-0001-0001-000000000003");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Gender", LanguageCode = "de", Value = "Maennlich" },
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Gender", LanguageCode = "en", Value = "Male" },
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Gender", LanguageCode = "de", Value = "Weiblich" },
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Gender", LanguageCode = "en", Value = "Female" },
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Gender", LanguageCode = "de", Value = "Divers" },
            new Translation { Id = Guid.Parse("00000020-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Gender", LanguageCode = "en", Value = "Non-binary" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="MaritalStatus"/> entities.
    /// </summary>
    private static void SeedMaritalStatusTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000030-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000030-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000030-0001-0001-0001-000000000003");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "MaritalStatus", LanguageCode = "de", Value = "Ledig" },
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "MaritalStatus", LanguageCode = "en", Value = "Single" },
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "MaritalStatus", LanguageCode = "de", Value = "Verheiratet" },
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "MaritalStatus", LanguageCode = "en", Value = "Married" },
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "MaritalStatus", LanguageCode = "de", Value = "Geschieden" },
            new Translation { Id = Guid.Parse("00000030-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "MaritalStatus", LanguageCode = "en", Value = "Divorced" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="Country"/> entities.
    /// </summary>
    private static void SeedCountryTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000040-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000040-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000040-0001-0001-0001-000000000003");
        var id4 = Guid.Parse("00000040-0001-0001-0001-000000000004");
        var id5 = Guid.Parse("00000040-0001-0001-0001-000000000005");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Country", LanguageCode = "de", Value = "Deutschland" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Country", LanguageCode = "en", Value = "Germany" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Country", LanguageCode = "de", Value = "Oesterreich" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Country", LanguageCode = "en", Value = "Austria" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Country", LanguageCode = "de", Value = "Schweiz" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Country", LanguageCode = "en", Value = "Switzerland" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000007"), EntityId = id4, EntityType = "Country", LanguageCode = "de", Value = "Vereinigtes Koenigreich" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000008"), EntityId = id4, EntityType = "Country", LanguageCode = "en", Value = "United Kingdom" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000009"), EntityId = id5, EntityType = "Country", LanguageCode = "de", Value = "Vereinigte Staaten" },
            new Translation { Id = Guid.Parse("00000040-0002-0001-0001-000000000010"), EntityId = id5, EntityType = "Country", LanguageCode = "en", Value = "United States" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="AddressType"/> entities.
    /// </summary>
    private static void SeedAddressTypeTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000050-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000050-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000050-0001-0001-0001-000000000003");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "AddressType", LanguageCode = "de", Value = "Privat" },
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "AddressType", LanguageCode = "en", Value = "Private" },
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "AddressType", LanguageCode = "de", Value = "Geschaeftlich" },
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "AddressType", LanguageCode = "en", Value = "Business" },
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "AddressType", LanguageCode = "de", Value = "Lieferung" },
            new Translation { Id = Guid.Parse("00000050-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "AddressType", LanguageCode = "en", Value = "Delivery" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="ContactType"/> entities.
    /// </summary>
    private static void SeedContactTypeTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000060-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000060-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000060-0001-0001-0001-000000000003");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "ContactType", LanguageCode = "de", Value = "Mobil" },
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "ContactType", LanguageCode = "en", Value = "Mobile" },
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "ContactType", LanguageCode = "de", Value = "Festnetz" },
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "ContactType", LanguageCode = "en", Value = "Landline" },
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "ContactType", LanguageCode = "de", Value = "Fax" },
            new Translation { Id = Guid.Parse("00000060-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "ContactType", LanguageCode = "en", Value = "Fax" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="Industry"/> entities.
    /// </summary>
    private static void SeedIndustryTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000070-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000070-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000070-0001-0001-0001-000000000003");
        var id4 = Guid.Parse("00000070-0001-0001-0001-000000000004");
        var id5 = Guid.Parse("00000070-0001-0001-0001-000000000005");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Industry", LanguageCode = "de", Value = "Technologie" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Industry", LanguageCode = "en", Value = "Technology" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Industry", LanguageCode = "de", Value = "Finanzen" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Industry", LanguageCode = "en", Value = "Finance" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Industry", LanguageCode = "de", Value = "Beratung" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Industry", LanguageCode = "en", Value = "Consulting" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000007"), EntityId = id4, EntityType = "Industry", LanguageCode = "de", Value = "Produktion" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000008"), EntityId = id4, EntityType = "Industry", LanguageCode = "en", Value = "Manufacturing" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000009"), EntityId = id5, EntityType = "Industry", LanguageCode = "de", Value = "Gesundheitswesen" },
            new Translation { Id = Guid.Parse("00000070-0002-0001-0001-000000000010"), EntityId = id5, EntityType = "Industry", LanguageCode = "en", Value = "Healthcare" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="LegalForm"/> entities.
    /// </summary>
    private static void SeedLegalFormTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000080-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000080-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000080-0001-0001-0001-000000000003");
        var id4 = Guid.Parse("00000080-0001-0001-0001-000000000004");
        var id5 = Guid.Parse("00000080-0001-0001-0001-000000000005");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "LegalForm", LanguageCode = "de", Value = "GmbH" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "LegalForm", LanguageCode = "en", Value = "Ltd" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "LegalForm", LanguageCode = "de", Value = "AG" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "LegalForm", LanguageCode = "en", Value = "Corp" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "LegalForm", LanguageCode = "de", Value = "UG" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "LegalForm", LanguageCode = "en", Value = "Ltd (limited)" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000007"), EntityId = id4, EntityType = "LegalForm", LanguageCode = "de", Value = "Ltd" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000008"), EntityId = id4, EntityType = "LegalForm", LanguageCode = "en", Value = "Ltd" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000009"), EntityId = id5, EntityType = "LegalForm", LanguageCode = "de", Value = "Einzelunternehmen" },
            new Translation { Id = Guid.Parse("00000080-0002-0001-0001-000000000010"), EntityId = id5, EntityType = "LegalForm", LanguageCode = "en", Value = "Sole Proprietorship" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="Department"/> entities.
    /// </summary>
    private static void SeedDepartmentTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("00000090-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("00000090-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("00000090-0001-0001-0001-000000000003");
        var id4 = Guid.Parse("00000090-0001-0001-0001-000000000004");
        var id5 = Guid.Parse("00000090-0001-0001-0001-000000000005");

        builder.HasData(
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Department", LanguageCode = "de", Value = "Vertrieb" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Department", LanguageCode = "en", Value = "Sales" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Department", LanguageCode = "de", Value = "IT" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Department", LanguageCode = "en", Value = "IT" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Department", LanguageCode = "de", Value = "Personal" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Department", LanguageCode = "en", Value = "HR" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000007"), EntityId = id4, EntityType = "Department", LanguageCode = "de", Value = "Finanzen" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000008"), EntityId = id4, EntityType = "Department", LanguageCode = "en", Value = "Finance" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000009"), EntityId = id5, EntityType = "Department", LanguageCode = "de", Value = "Marketing" },
            new Translation { Id = Guid.Parse("00000090-0002-0001-0001-000000000010"), EntityId = id5, EntityType = "Department", LanguageCode = "en", Value = "Marketing" });
    }

    /// <summary>
    /// Seeds translation data for <see cref="Position"/> entities.
    /// </summary>
    private static void SeedPositionTranslations(EntityTypeBuilder<Translation> builder)
    {
        var id1 = Guid.Parse("000000a0-0001-0001-0001-000000000001");
        var id2 = Guid.Parse("000000a0-0001-0001-0001-000000000002");
        var id3 = Guid.Parse("000000a0-0001-0001-0001-000000000003");
        var id4 = Guid.Parse("000000a0-0001-0001-0001-000000000004");
        var id5 = Guid.Parse("000000a0-0001-0001-0001-000000000005");

        builder.HasData(
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000001"), EntityId = id1, EntityType = "Position", LanguageCode = "de", Value = "Geschaeftsfuehrer" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000002"), EntityId = id1, EntityType = "Position", LanguageCode = "en", Value = "CEO" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000003"), EntityId = id2, EntityType = "Position", LanguageCode = "de", Value = "Manager" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000004"), EntityId = id2, EntityType = "Position", LanguageCode = "en", Value = "Manager" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000005"), EntityId = id3, EntityType = "Position", LanguageCode = "de", Value = "Entwickler" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000006"), EntityId = id3, EntityType = "Position", LanguageCode = "en", Value = "Developer" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000007"), EntityId = id4, EntityType = "Position", LanguageCode = "de", Value = "Berater" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000008"), EntityId = id4, EntityType = "Position", LanguageCode = "en", Value = "Consultant" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000009"), EntityId = id5, EntityType = "Position", LanguageCode = "de", Value = "Assistent" },
            new Translation { Id = Guid.Parse("000000a0-0002-0001-0001-000000000010"), EntityId = id5, EntityType = "Position", LanguageCode = "en", Value = "Assistant" });
    }
}
