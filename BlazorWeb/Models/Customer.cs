using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Name { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Email { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Phone { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Address { get; set; }
}