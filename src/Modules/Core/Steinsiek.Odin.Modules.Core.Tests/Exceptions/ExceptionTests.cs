namespace Steinsiek.Odin.Modules.Core.Tests.Exceptions;

/// <summary>
/// Unit tests for the Odin exception hierarchy.
/// </summary>
[TestClass]
public sealed class ExceptionTests
{
    [TestMethod]
    public void OdinException_ShouldSetMessageStatusCodeAndErrorType()
    {
        // Arrange & Act
        var exception = new OdinException("Test error", 500, OdinErrorType.Internal);

        // Assert
        Assert.AreEqual("Test error", exception.Message);
        Assert.AreEqual(500, exception.StatusCode);
        Assert.AreEqual(OdinErrorType.Internal, exception.ErrorType);
    }

    [TestMethod]
    public void OdinException_WithInnerException_ShouldPreserveInner()
    {
        // Arrange
        var inner = new InvalidOperationException("Inner error");

        // Act
        var exception = new OdinException("Outer error", 500, OdinErrorType.Internal, inner);

        // Assert
        Assert.AreEqual("Outer error", exception.Message);
        Assert.AreEqual(500, exception.StatusCode);
        Assert.AreSame(inner, exception.InnerException);
    }

    [TestMethod]
    public void ElementNotFoundException_ShouldHave404StatusCode()
    {
        // Arrange & Act
        var exception = new ElementNotFoundException("Resource not found");

        // Assert
        Assert.AreEqual(404, exception.StatusCode);
        Assert.AreEqual(OdinErrorType.NotFound, exception.ErrorType);
        Assert.AreEqual("Resource not found", exception.Message);
    }

    [TestMethod]
    public void ElementNotFoundException_WithEntityAndId_ShouldFormatMessage()
    {
        // Arrange
        var id = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var exception = new ElementNotFoundException("Person", id);

        // Assert
        Assert.AreEqual("Person with id '12345678-1234-1234-1234-123456789abc' was not found", exception.Message);
        Assert.AreEqual(404, exception.StatusCode);
    }

    [TestMethod]
    public void OdinValidationException_ShouldHave400StatusCode()
    {
        // Arrange & Act
        var exception = new OdinValidationException("Invalid input");

        // Assert
        Assert.AreEqual(400, exception.StatusCode);
        Assert.AreEqual(OdinErrorType.Validation, exception.ErrorType);
        Assert.AreEqual("Invalid input", exception.Message);
    }

    [TestMethod]
    public void BusinessRuleException_ShouldHave400StatusCode()
    {
        // Arrange & Act
        var exception = new BusinessRuleException("Rule violated");

        // Assert
        Assert.AreEqual(400, exception.StatusCode);
        Assert.AreEqual(OdinErrorType.BusinessRule, exception.ErrorType);
        Assert.AreEqual("Rule violated", exception.Message);
    }
}
