using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Customer;
using RetailManagement_be.Models.DTOs.Product;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence.Interfaces;
using RetailManagement_be.Shared.Exceptions;

namespace RetailManagement_be.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto?> GetCustomerById(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);

        if (customer is null)
            return null;

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
        };
    }

    public async Task<List<CustomerDto>> GetCustomers()
    {
        return (await _unitOfWork.Customers.GetAllAsync())
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,     
                Email = c.Email,
            }).ToList();
    }

    public async Task<int> AddCustomer(CreateCustomerDto customerDto)
    {
        if (string.IsNullOrWhiteSpace(customerDto.Name))
            throw new ValidationException("Customer name is required");

        var customer = new Customer
        {
            Name = customerDto.Name,
            Email = customerDto.Email,
        };

        await _unitOfWork.Customers.AddAsync(customer);

        await _unitOfWork.CompleteAsync();

        return customer.Id;
    }

    public async Task UpdateCustomer(UpdateCustomerDto customerDto)
    {
        var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(customerDto.Id) ?? throw new NotFoundException<Customer>();
        
        if (!string.IsNullOrWhiteSpace(customerDto.Name))
            existingCustomer.Name = customerDto.Name;

        if (!string.IsNullOrWhiteSpace(customerDto.Email))
            existingCustomer.Email = customerDto.Email;

        await _unitOfWork.Customers.UpdateAsync(existingCustomer);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new NotFoundException<Customer>();

        // Check if the customer has any purchases before deleting
        bool hasPurchases = await _unitOfWork.Purchases.HasPurchasesForCustomer(id);
        if (hasPurchases)
            throw new ValidationException("Cannot delete customer with existing purchases.");

        await _unitOfWork.Customers.DeleteAsync(customer.Id);

        await _unitOfWork.CompleteAsync();
    }
}
