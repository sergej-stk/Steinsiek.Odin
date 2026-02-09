namespace Steinsiek.Odin.Modules.Core.Exceptions;

/// <summary>
/// Exception thrown when a requested element is not found.
/// </summary>
public sealed class ElementNotFoundException : OdinException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The error message describing which element was not found.</param>
    public ElementNotFoundException(string message)
        : base(message, 404, OdinErrorType.NotFound)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class for a specific entity.
    /// </summary>
    /// <param name="entityName">The name of the entity type.</param>
    /// <param name="id">The identifier that was not found.</param>
    public ElementNotFoundException(string entityName, Guid id)
        : base($"{entityName} with id '{id}' was not found", 404, OdinErrorType.NotFound)
    {
    }
}
