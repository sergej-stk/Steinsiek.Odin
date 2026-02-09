namespace Steinsiek.Odin.Modules.Products.Tests;

/// <summary>
/// Unit tests for <see cref="ProductService"/>.
/// </summary>
[TestClass]
public sealed class ProductServiceTests
{
    private Mock<IProductRepository> _productRepositoryMock = null!;
    private Mock<ICategoryRepository> _categoryRepositoryMock = null!;
    private Mock<ILogger<ProductService>> _loggerMock = null!;
    private ProductService _sut = null!;

    /// <summary>
    /// Initializes mock dependencies before each test.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _loggerMock = new Mock<ILogger<ProductService>>();
        _sut = new ProductService(_productRepositoryMock.Object, _categoryRepositoryMock.Object, _loggerMock.Object);
    }

    /// <summary>
    /// Verifies that GetAll returns all products with total count.
    /// </summary>
    [TestMethod]
    public async Task GetAll_ReturnsAllProductsWithTotalCount()
    {
        var categoryId = Guid.NewGuid();
        var products = new List<Product>
        {
            new() { Name = "Product 1", CategoryId = categoryId, Price = 10m },
            new() { Name = "Product 2", CategoryId = categoryId, Price = 20m }
        };
        var category = new Category { Name = "Electronics" };

        _productRepositoryMock
            .Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        _categoryRepositoryMock
            .Setup(r => r.GetById(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var result = await _sut.GetAll(CancellationToken.None);

        Assert.AreEqual(2, result.TotalCount);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Product 1", result.Data[0].Name);
        Assert.AreEqual("Electronics", result.Data[0].CategoryName);
    }

    /// <summary>
    /// Verifies that GetById returns null for a non-existent product.
    /// </summary>
    [TestMethod]
    public async Task GetById_NonExistent_ReturnsNull()
    {
        var id = Guid.NewGuid();
        _productRepositoryMock
            .Setup(r => r.GetById(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var result = await _sut.GetById(id, CancellationToken.None);

        Assert.IsNull(result);
    }

    /// <summary>
    /// Verifies that GetById returns the product with category name.
    /// </summary>
    [TestMethod]
    public async Task GetById_ExistingProduct_ReturnsProductWithCategoryName()
    {
        var categoryId = Guid.NewGuid();
        var product = new Product { Name = "Test Product", CategoryId = categoryId, Price = 15m };
        var category = new Category { Name = "Books" };

        _productRepositoryMock
            .Setup(r => r.GetById(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);
        _categoryRepositoryMock
            .Setup(r => r.GetById(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var result = await _sut.GetById(product.Id, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual("Test Product", result.Name);
        Assert.AreEqual("Books", result.CategoryName);
    }

    /// <summary>
    /// Verifies that Create adds the product and returns the DTO.
    /// </summary>
    [TestMethod]
    public async Task Create_ValidRequest_ReturnsCreatedProduct()
    {
        var categoryId = Guid.NewGuid();
        var request = new CreateProductRequest
        {
            Name = "New Product",
            Description = "A new product",
            Price = 29.99m,
            Stock = 100,
            CategoryId = categoryId
        };
        var category = new Category { Name = "Electronics" };

        _productRepositoryMock
            .Setup(r => r.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product p, CancellationToken _) => p);
        _categoryRepositoryMock
            .Setup(r => r.GetById(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var result = await _sut.Create(request, CancellationToken.None);

        Assert.AreEqual("New Product", result.Name);
        Assert.AreEqual(29.99m, result.Price);
        Assert.AreEqual("Electronics", result.CategoryName);
        _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Verifies that Delete returns true when the product exists.
    /// </summary>
    [TestMethod]
    public async Task Delete_ExistingProduct_ReturnsTrue()
    {
        var id = Guid.NewGuid();
        _productRepositoryMock
            .Setup(r => r.Delete(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _sut.Delete(id, CancellationToken.None);

        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that Delete returns false when the product does not exist.
    /// </summary>
    [TestMethod]
    public async Task Delete_NonExistentProduct_ReturnsFalse()
    {
        var id = Guid.NewGuid();
        _productRepositoryMock
            .Setup(r => r.Delete(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _sut.Delete(id, CancellationToken.None);

        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that Search returns matching products with total count.
    /// </summary>
    [TestMethod]
    public async Task Search_MatchingTerm_ReturnsMatchingProducts()
    {
        var categoryId = Guid.NewGuid();
        var products = new List<Product>
        {
            new() { Name = "Laptop Pro", CategoryId = categoryId, Price = 999m }
        };
        var category = new Category { Name = "Electronics" };

        _productRepositoryMock
            .Setup(r => r.Search("Laptop", It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        _categoryRepositoryMock
            .Setup(r => r.GetById(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var result = await _sut.Search("Laptop", CancellationToken.None);

        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual("Laptop Pro", result.Data[0].Name);
    }
}
