using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.DTOs.Product;

public class UpdateProductDto
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }    

    [Precision(18, 2)]
    public decimal Price { get; set; }
}
