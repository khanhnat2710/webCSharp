using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public DateTime OrderDate { get; set; } = DateTime.Today;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public int? PaymentId { get; set; }
    public int? AdminId { get; set; }
    public int? CustomerId { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }
    public Admin? Admin { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

public enum OrderStatus : byte
{
    Pending = 0,
    Confirmed = 1,
    Shipping = 2,
    Completed = 3,
    Cancelled = 4
}
