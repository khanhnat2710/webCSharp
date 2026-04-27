using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public String Name { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public String Description { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}