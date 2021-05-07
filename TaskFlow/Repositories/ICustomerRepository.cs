using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Delete(int customerId);
        List<Customer> GetAll();
        Customer GetById(int id);
        void Update(Customer customer);

        List<Customer> Search(string criterion, bool sortDescending);

        Customer GetCustomerByIdWithAddressWithJob(int id);

        List<Customer> getCustomersWithAddress();
    }
}