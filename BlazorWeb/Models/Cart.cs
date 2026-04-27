using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; } = DateTime.Today;

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
