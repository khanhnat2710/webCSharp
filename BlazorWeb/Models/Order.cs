using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
    [Display(Name = "Chờ xác nhận")]
    Pending = 0,
    [Display(Name = "Đã xác nhận")]
    Confirmed = 1,
    [Display(Name = "Đang giao")]
    Shipping = 2,
    [Display(Name = "Hoàn thành")]
    Completed = 3,
    [Display(Name = "Đã hủy")]
    Cancelled = 4
}

public static class OrderStatusExtensions
{
    public static string GetDisplayName(this OrderStatus status)
    {
        var member = typeof(OrderStatus).GetMember(status.ToString()).FirstOrDefault();
        var displayAttribute = member?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? status.ToString();
    }
}
