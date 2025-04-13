namespace Domain.Entities
{
  public class Order : BaseEntity
  {
    public string OrderId { get; set; }  // Order Reference Number
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public decimal Price { get; set; } // Total Order Value
    public string Note { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public ICollection<OrderDetail> OrderDetails { get; set; }
  }
}