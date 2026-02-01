namespace Steinsiek.Odin.Modules.Products.Services;

/// <summary>
/// Implementation of the product service handling CRUD operations.
/// </summary>
public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<ProductService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="categoryRepository">The category repository.</param>
    /// <param name="logger">The logger instance.</param>
    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductDto>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll(cancellationToken);
        var dtos = new List<ProductDto>();

        foreach (var product in products)
        {
            var category = await _categoryRepository.GetById(product.CategoryId, cancellationToken);
            dtos.Add(MapToDto(product, category?.Name));
        }

        return dtos;
    }

    /// <inheritdoc />
    public async Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);
        if (product is null) return null;

        var category = await _categoryRepository.GetById(product.CategoryId, cancellationToken);
        return MapToDto(product, category?.Name);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductDto>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByCategoryId(categoryId, cancellationToken);
        var category = await _categoryRepository.GetById(categoryId, cancellationToken);
        return products.Select(p => MapToDto(p, category?.Name));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductDto>> Search(string searchTerm, CancellationToken cancellationToken)
    {
        var products = await _productRepository.Search(searchTerm, cancellationToken);
        var dtos = new List<ProductDto>();

        foreach (var product in products)
        {
            var category = await _categoryRepository.GetById(product.CategoryId, cancellationToken);
            dtos.Add(MapToDto(product, category?.Name));
        }

        return dtos;
    }

    /// <inheritdoc />
    public async Task<ProductDto> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating product: {Name}", request.Name);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };

        await _productRepository.Add(product, cancellationToken);
        _logger.LogInformation("Product created with ID: {Id}", product.Id);

        var category = await _categoryRepository.GetById(product.CategoryId, cancellationToken);
        return MapToDto(product, category?.Name);
    }

    /// <inheritdoc />
    public async Task<ProductDto?> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);
        if (product is null)
        {
            _logger.LogWarning("Product not found: {Id}", id);
            return null;
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.ImageUrl = request.ImageUrl;
        product.CategoryId = request.CategoryId;
        product.IsActive = request.IsActive;

        await _productRepository.Update(product, cancellationToken);
        _logger.LogInformation("Product updated: {Id}", id);

        var category = await _categoryRepository.GetById(product.CategoryId, cancellationToken);
        return MapToDto(product, category?.Name);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.Delete(id, cancellationToken);
        if (result)
        {
            _logger.LogInformation("Product deleted: {Id}", id);
        }
        return result;
    }

    private static ProductDto MapToDto(Product product, string? categoryName) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Stock = product.Stock,
        ImageUrl = product.ImageUrl,
        CategoryId = product.CategoryId,
        CategoryName = categoryName,
        IsActive = product.IsActive
    };
}
