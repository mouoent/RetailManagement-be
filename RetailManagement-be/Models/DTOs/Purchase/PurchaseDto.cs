namespace RetailManagement_be.Models.DTOs.Purchase;

public class PurchaseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<int> ProductIds { get; set; }
}
