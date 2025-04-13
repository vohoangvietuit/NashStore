using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
  private readonly IProductService _productService;
  private readonly IWebHostEnvironment _environment;

  public ProductsController(IProductService productService, IWebHostEnvironment environment)
  {
    _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    _environment = environment;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await _productService.GetProductByIdAsync(id);

    if (product == null)
    {
      return NotFound();
    }

    return Ok(product);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page = 1, int pageSize = 10, string? searchTerm = null, int? categoryId = null)
  {
    var (products, totalCount) = await _productService.GetPaginatedProductsAsync(page, pageSize, searchTerm, categoryId);

    // Return products with pagination information
    return Ok(new
    {
      Data = products,
      TotalRecord = totalCount,
      PageSize = pageSize,
      CurrentPage = page
    });
  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductDto productDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    string? imageUrl = null;
    if (productDto.Image != null && productDto.Image.Length > 0)
    {
      imageUrl = await SaveImage(productDto.Image);
    }

    var product = new Product
    {
      Name = productDto.Name,
      Price = productDto.Price,
      Quantity = productDto.Quantity,
      Note = productDto.Note,
      CategoryId = productDto.CategoryId,
      Image = imageUrl, // Set the image URL
      Date = DateTime.UtcNow
    };

    await _productService.CreateProductAsync(product);

    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
  }

  [Authorize(Roles = "Admin")]
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var existingProduct = await _productService.GetProductByIdAsync(id);

    if (existingProduct == null)
    {
      return NotFound();
    }

    string? imageUrl = existingProduct.Image; // Keep existing image URL if no new image is uploaded

    if (productDto.Image != null && productDto.Image.Length > 0)
    {
      // Save the new image and get the new URL
      imageUrl = await SaveImage(productDto.Image);
    }
    else
    {
      // If no new image is uploaded and no existing image, set imageUrl to null or a default value
      imageUrl = existingProduct.Image;
    }

    // Update the product properties
    existingProduct.Name = productDto.Name;
    existingProduct.Price = productDto.Price;
    existingProduct.Quantity = productDto.Quantity;
    existingProduct.Note = productDto.Note;
    existingProduct.CategoryId = productDto.CategoryId;
    existingProduct.Image = imageUrl;

    await _productService.UpdateProductAsync(existingProduct);

    return NoContent();
  }

  [Authorize(Roles = "Admin")]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteProduct(int id)
  {
    var product = await _productService.GetProductByIdAsync(id);

    if (product == null)
    {
      return NotFound();
    }

    await _productService.DeleteProductAsync(id);

    return NoContent();
  }

  // Handle the image upload and save logic
  private async Task<string> SaveImage(IFormFile image)
  {
    try
    {
      var folderName = Path.Combine("wwwroot", "uploads");
      var pathToSave = Path.Combine(_environment.ContentRootPath, folderName);

      if (!Directory.Exists(pathToSave))
      {
        Directory.CreateDirectory(pathToSave);
      }

      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
      var fullPath = Path.Combine(pathToSave, fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        await image.CopyToAsync(stream);
      }

      return Path.Combine("uploads", fileName); // Relative path
    }
    catch (Exception ex)
    {
      // Log the exception
      Console.WriteLine($"Image upload failed: {ex.Message}");
      return null;
    }
  }
  [HttpGet("suggest-product")]
  public async Task<ActionResult<IEnumerable<Product>>> SuggestProduct(string? search = null, int? category = null)
  {
    if (string.IsNullOrEmpty(search))
    {
      return Ok(new List<Product>());
    }

    var products = await _productService.SearchProductsAsync(search);
    return Ok(products);
  }
}
