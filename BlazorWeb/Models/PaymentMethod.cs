using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class PaymentMethod
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
