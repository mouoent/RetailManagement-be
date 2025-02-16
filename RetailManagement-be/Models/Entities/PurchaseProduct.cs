namespace RetailManagement_be.Models.Entities;

public class PurchaseProduct : BaseEntity
{
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }  
}
