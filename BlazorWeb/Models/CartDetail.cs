using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class CartDetail
{
    [Required]
    public int? CartId { get; set; }

    [Required]
    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
}
