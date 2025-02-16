using RetailManagement_be.Shared.Exceptions;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Product;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto?> GetProductById(int id)
    {
        var product =  await _unitOfWork.Products.GetByIdAsync(id);

        if (product is null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        return (await _unitOfWork.Products.GetAllAsync())
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToList();
    }

    public async Task<int> AddProduct(CreateProductDto productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.Name))
            throw new ValidationException("Product name is required");
        if (productDto.Price <= 0)
            throw new ValidationException("Product price must be greater than zero");

        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
        };

        await _unitOfWork.Products.AddAsync(product);

        await _unitOfWork.CompleteAsync();

        return product.Id;
    }

    public async Task UpdateProduct(UpdateProductDto productDto)
    {
        var existingProduct = await _unitOfWork.Products.GetByIdAsync(productDto.Id) ?? throw new NotFoundException<Product>();

        if (!string.IsNullOrWhiteSpace(productDto.Name))
            existingProduct.Name = productDto.Name;

        if (productDto.Price > 0)
            existingProduct.Price = productDto.Price;

        await _unitOfWork.Products.UpdateAsync(existingProduct);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id) ?? throw new NotFoundException<Product>();

        // Ensure product isn't linked to any purchases
        bool isLinkedToPurchases = await _unitOfWork.PurchaseProducts.IsProductLinkedToPurchases(id);
        if (isLinkedToPurchases)
            throw new ValidationException("Cannot delete product linked to existing purchases");

        await _unitOfWork.Products.DeleteAsync(product.Id);

        await _unitOfWork.CompleteAsync();
    }
}
