using RetailManagement_be.Models.DTOs.Customer;

namespace RetailManagement_be.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerById(int id);
        Task<int> AddCustomer(CreateCustomerDto customerDto);
        Task DeleteCustomer(int id);
        Task<List<CustomerDto>> GetCustomers();
        Task UpdateCustomer(UpdateCustomerDto customerDto);
    }
}