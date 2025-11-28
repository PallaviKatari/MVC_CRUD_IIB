using MVC_Demo.Models;
using System.Xml.Linq;

namespace MVC_Demo.Services
{
    //Service for In-Memory List
    public class CustomerService
    {
        private readonly List<Customer> _customers = new();

        public List<Customer> GetAll() => _customers;

        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

        public void Add(Customer customer)
        {
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.Id);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Email = customer.Email;
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null) _customers.Remove(existing);
        }
    }
}
