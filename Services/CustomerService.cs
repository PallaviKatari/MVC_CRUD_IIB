using MVC_Demo.Interfaces;
using MVC_Demo.Models;
using System.Xml.Linq;

namespace MVC_Demo.Services
{
    //Business Logic - CRUD Operations from DB or In-Memory List
    //Service for In-Memory List
    public class CustomerService : IGetService
    {
        private readonly List<Customer> _customers = new();

        //Get All Customers
        public List<Customer> GetAll() => _customers;

        //Get Customer by Id
        //Lambda Expression
        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

        public void Add(Customer customer)
        {
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.Id); // 1
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Email = customer.Email;
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id); //1
            if (existing != null) _customers.Remove(existing);
            DisplayMessage();
        }

        public void DisplayMessage()
        {
            Console.WriteLine("Customer Service Message");
        }
    }
}
