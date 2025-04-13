namespace Domain.Entities
{
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Note { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string? Image { get; set; } // URL or path
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
}