using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
  public class ProductDto
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Note { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }  // Optional new image
  }
}