using RetailManagement_be.Models.DTOs.Product;

namespace RetailManagement_be.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductById(int id);
        Task<int> AddProduct(CreateProductDto productDto);
        Task DeleteProduct(int id);
        Task<List<ProductDto>> GetProducts();
        Task UpdateProduct(UpdateProductDto productDto);
    }
}