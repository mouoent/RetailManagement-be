using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Purchase;

public class CreatePurchaseDto
{
    [Required]
    public int CustomerId { get; set; }   
    public List<int> ProductIds { get; set; } = new();
}
