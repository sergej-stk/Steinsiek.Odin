namespace Steinsiek.Odin.Modules.Products.Controllers;

/// <summary>
/// API controller for product operations.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class ProductsController(IProductService productService) : ControllerBase, IProductsController
{
    private readonly IProductService _productService = productService;

    /// <inheritdoc />
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAll(cancellationToken);
        return Ok(products);
    }

    /// <inheritdoc />
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetById(id, cancellationToken);
        return product is null ? (ActionResult<ProductDto>)NotFound() : (ActionResult<ProductDto>)product;
    }

    /// <inheritdoc />
    [HttpGet("category/{categoryId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<ProductDto>>> GetByCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = await _productService.GetByCategoryId(categoryId, cancellationToken);
        return Ok(products);
    }

    /// <inheritdoc />
    [HttpGet("search")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<ProductDto>>> Search([FromQuery] string? q, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return Ok(new ListResult<ProductDto> { TotalCount = 0, Data = [] });
        }
        var products = await _productService.Search(q, cancellationToken);
        return Ok(products);
    }

    /// <inheritdoc />
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.Create(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <inheritdoc />
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.Update(id, request, cancellationToken);
        return product ?? (ActionResult<ProductDto>)NotFound();
    }

    /// <inheritdoc />
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.Delete(id, cancellationToken);
        return !result ? NotFound() : NoContent();
    }
}
