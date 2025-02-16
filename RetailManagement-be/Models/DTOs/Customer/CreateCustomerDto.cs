using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Customer;

public class CreateCustomerDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
