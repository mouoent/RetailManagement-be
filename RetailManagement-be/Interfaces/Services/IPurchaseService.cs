using RetailManagement_be.Models.DTOs.Purchase;

namespace RetailManagement_be.Interfaces.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDto> AddPurchase(CreatePurchaseDto purchaseDto);
        Task DeletePurchase(int id);
    }
}