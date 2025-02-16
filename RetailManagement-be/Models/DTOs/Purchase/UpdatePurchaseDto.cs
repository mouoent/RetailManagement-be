using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Purchase;

public class UpdatePurchaseDto
{
    [Required]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<int> ProductIds { get; set; } = new();
}
