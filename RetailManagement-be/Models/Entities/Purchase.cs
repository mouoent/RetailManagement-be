namespace RetailManagement_be.Models.Entities;

public class Purchase : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }    
    public List<PurchaseProduct> PurchaseProducts { get; set; } = new();
}
