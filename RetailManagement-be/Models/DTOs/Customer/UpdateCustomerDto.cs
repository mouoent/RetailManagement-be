using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Customer;

public class UpdateCustomerDto
{
    [Required]
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
}
