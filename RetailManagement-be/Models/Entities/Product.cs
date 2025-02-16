using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.Entities;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    public List<PurchaseProduct> PurchaseProducts { get; set; } = new();
}
