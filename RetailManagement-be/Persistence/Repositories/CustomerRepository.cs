using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(RetailManagementDbContext context) : base(context)
    {
    }
}
