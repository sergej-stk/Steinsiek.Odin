namespace Steinsiek.Odin.Modules.Products.Services;

/// <summary>
/// Implementation of the category service handling CRUD operations.
/// </summary>
public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="categoryRepository">The category repository.</param>
    /// <param name="logger">The logger instance.</param>
    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll(cancellationToken);
        return categories.Select(MapToDto);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(id, cancellationToken);
        return category is null ? null : MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating category: {Name}", request.Name);

        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await _categoryRepository.Add(category, cancellationToken);
        _logger.LogInformation("Category created with ID: {Id}", category.Id);

        return MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(id, cancellationToken);
        if (category is null)
        {
            _logger.LogWarning("Category not found: {Id}", id);
            return null;
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsActive = request.IsActive;

        await _categoryRepository.Update(category, cancellationToken);
        _logger.LogInformation("Category updated: {Id}", id);

        return MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.Delete(id, cancellationToken);
        if (result)
        {
            _logger.LogInformation("Category deleted: {Id}", id);
        }
        return result;
    }

    private static CategoryDto MapToDto(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        IsActive = category.IsActive
    };
}
