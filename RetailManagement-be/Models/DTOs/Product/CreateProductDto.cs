using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Precision(18, 2)]
    public decimal Price { get; set; } = decimal.Zero;
}
