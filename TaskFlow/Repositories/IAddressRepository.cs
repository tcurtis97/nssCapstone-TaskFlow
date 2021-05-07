using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface IAddressRepository
    {
        void Add(CustomerAddress address);
        void Delete(int addressId);
        List<CustomerAddress> GetAll();
        void Update(CustomerAddress address);
        List<CustomerAddress> GetAllAddressesByCustomerId(int id);
    }
}