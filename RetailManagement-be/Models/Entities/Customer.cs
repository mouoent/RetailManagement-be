using System.ComponentModel.DataAnnotations;

namespace RetailManagement_be.Models.Entities;

public class Customer : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
}
